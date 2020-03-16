using MongoDB.Driver;
using RoomService.Models;
using RoomService.Settings;
using RoomService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace RoomService.Services
{
    public class ReservationUpdaterService
    {
        private readonly IMongoCollection<Reservation> _reservationRepo;
        private readonly IMongoCollection<WorkSpace>   _workSpaceRepo;
        private readonly ServerTimeTaskData[] _serverTasks;
        private readonly ServerTaskUtils _serverTaskUtils;
        //@TODO use settings or database
        private readonly HashSet<Reservation.Statuses> _goingStatuses = new HashSet<Reservation.Statuses>
        { Reservation.Statuses.ATTIVA, Reservation.Statuses.CHECKIN, Reservation.Statuses.INCORSO };

        private readonly HashSet<Reservation.Statuses> _storedStatuses = new HashSet<Reservation.Statuses>
        { Reservation.Statuses.CANCELLATA, Reservation.Statuses.CONCLUSA };

        public ReservationUpdaterService(
            IRoomServiceMongoSettings mongoSettings, 
            IAppSettings appSettings, 
            ServerTaskUtils serverTaskService
        )
        {
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);

            _reservationRepo = database.GetCollection<Reservation>(mongoSettings.ReservationCollection);
            _workSpaceRepo   = database.GetCollection<WorkSpace>(mongoSettings.WorkSpaceCollection);
            _serverTasks     = appSettings.ServerTasks; //Use database for tasks???
            _serverTaskUtils = serverTaskService;

            this.UpdateReservation();
            InitTasks(DateTime.Today);
        }

        private bool InitTasks(DateTime time)
        {
            foreach (var task in _serverTasks)
                if(task.TaskType == TaskTypes.RESSTATUSUPDATE)
                    _serverTaskUtils.CreateTimeBasedServerTask(time.AddHours(task.Hour), UpdateReservation);

            _serverTaskUtils.CreateTimeBasedServerTask(time.AddHours(23).AddMinutes(59), LastJob);

            return true;
        }
        private bool LastJob()
            => InitTasks(DateTime.Today.AddDays(1));

        private bool UpdateReservation()
        {
            var now = DateTime.Now.ToString("o");

            var qres = from res in _reservationRepo.AsQueryable()
                       where this._goingStatuses.Contains(res.Status)
                       select res;

            if (qres == null)
                return false;

            foreach (var res in qres)
            {
                if (string.Compare(res.Day.StartTime, now) > 0) //Not yet started
                {
                    res.Status = Reservation.Statuses.ATTIVA;
                }
                if(string.Compare(res.Day.EndTime, now) <= 0) //Expired
                {
                    res.Status = Reservation.Statuses.CONCLUSA;
                }
                if(string.Compare(res.Day.StartTime, now) <= 0 && string.Compare(res.Day.EndTime, now) > 0) //CHECKIN or INCORSO
                {
                    res.Status =
                    res.Status == Reservation.Statuses.CHECKIN ?
                    Reservation.Statuses.CHECKIN : Reservation.Statuses.INCORSO;
                }
                _reservationRepo.ReplaceOne<Reservation>(dbres => res.Id == dbres.Id, res);
            }
            return true;
        }
    }
}
