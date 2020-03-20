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
    /// <summary>
    /// Update reservation statuses based on calendar/timer
    /// The tasks runs foreach time hook of type RESSTATUSUPDATE in settings
    /// </summary>
    public class ReservationUpdaterService
    {
        /// <summary>
        /// Required collection _reservationRepo is where reservations to update are stored
        /// </summary>
        private readonly IMongoCollection<Reservation> _reservationRepo;
        /// <summary>
        /// The server task array got from settings
        /// </summary>
        private readonly ServerTimeTaskData[] _serverTasks;
        /// <summary>
        /// Utility service for timer based task creation got from DI
        /// </summary>
        private readonly ServerTaskUtils _serverTaskUtils;
        /// <summary>
        /// _goingStatuses hashset helper 
        /// @TODO use settings or database
        /// </summary>
        private readonly HashSet<Reservation.Statuses> _goingStatuses = new HashSet<Reservation.Statuses>
        { Reservation.Statuses.ATTIVA, Reservation.Statuses.CHECKIN, Reservation.Statuses.INCORSO };
        /// <summary>
        /// Constructor use DI for required resources
        /// </summary>
        /// <param name="mongoSettings">The database settings data</param>
        /// <param name="appSettings">The app settings data</param>
        /// <param name="serverTaskService"></param>
        public ReservationUpdaterService(
            IRoomServiceMongoSettings mongoSettings, 
            IAppSettings appSettings, 
            ServerTaskUtils serverTaskService
        )
        {
            //Create a mongo client
            var client = new MongoClient(mongoSettings.ConnectionString);
            //Connect to target database
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            //Connect to target collection repo
            _reservationRepo = database.GetCollection<Reservation>(mongoSettings.ReservationCollection);
            //Tasks
            _serverTasks     = appSettings.ServerTasks; //Use database for tasks???
            _serverTaskUtils = serverTaskService;
            //Start an update and timer
            this.UpdateReservation();
            InitTasks(DateTime.Today);
        }
        /// <summary>
        /// Init tasks from array
        /// </summary>
        /// <param name="time">The ref start time</param>
        /// <returns>Bool : true success status</returns>
        private bool InitTasks(DateTime time)
        {
            foreach (var task in _serverTasks)
                if(task.TaskType == TaskTypes.RESSTATUSUPDATE)
                    _serverTaskUtils.CreateTimeBasedServerTask(time.AddHours(task.Hour), UpdateReservation);

            _serverTaskUtils.CreateTimeBasedServerTask(time.AddHours(23).AddMinutes(59), LastJob);

            return true;
        }
        /// <summary>
        /// This delegate is used for restart tasks for next day
        /// </summary>
        /// <returns>New task init result</returns>
        private bool LastJob()
            => InitTasks(DateTime.Today.AddDays(1));
        /// <summary>
        /// This delegate is used for update Reservation statuses on database
        /// </summary>
        /// <returns>Bool : true success/complete</returns>
        private bool UpdateReservation()
        {
            //Create comparison time
            var now = DateTime.Now.ToString("o");
            //Query for all on going statuses reservations
            var qres = from res in _reservationRepo.AsQueryable()
                       where this._goingStatuses.Contains(res.Status)
                       select res;
            //Query failed case
            if (qres == null)
                return false;
            //Foreach status to update
            foreach (var res in qres)
            {
                if (string.Compare(res.Interval.StartTime, now) > 0) //Not yet started set to active
                {
                    res.Status = Reservation.Statuses.ATTIVA;
                }
                if(string.Compare(res.Interval.EndTime, now) <= 0) //Expired set to ended
                {
                    res.Status = Reservation.Statuses.CONCLUSA;
                }
                if(string.Compare(res.Interval.StartTime, now) <= 0 && string.Compare(res.Interval.EndTime, now) > 0) //CHECKIN or INCORSO
                {
                    res.Status =
                    res.Status == Reservation.Statuses.CHECKIN ? //Is cheched-in? or waiting for fist check-in
                    Reservation.Statuses.CHECKIN : Reservation.Statuses.INCORSO;
                }
                //Store update in database
                _reservationRepo.ReplaceOne<Reservation>(dbres => res.Id == dbres.Id, res);
            }
            //Task complete
            return true;
        }
    }
}
