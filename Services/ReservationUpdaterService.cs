using MongoDB.Driver;
using RoomService.Models;
using RoomService.Settings;
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
        //@TODO: Timers Setup and dynamic delegate events
        public ReservationUpdaterService(IRoomServiceMongoSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _reservationRepo = database.GetCollection<Reservation>(settings.ReservationCollection);
            _workSpaceRepo   = database.GetCollection<WorkSpace>(settings.WorkSpaceCollection);

            //this.UpdateReservation(null, null);
            //InitTasks(DateTime.Now);
        }
        //@TODO generic task use vector of timers
        private void InitTasks(DateTime dayTask)
        {
            DateTime startMorning   = new DateTime(dayTask.Year, dayTask.Month, dayTask.Day, 9, 00, 0, 0);
            DateTime endMorning     = new DateTime(dayTask.Year, dayTask.Month, dayTask.Day, 13, 00, 0, 0);
            DateTime startAfternoon = new DateTime(dayTask.Year, dayTask.Month, dayTask.Day, 14, 00, 0, 0);
            DateTime endaftArnoon   = new DateTime(dayTask.Year, dayTask.Month, dayTask.Day, 18, 00, 0, 0);
            DateTime lastTask       = new DateTime(dayTask.Year, dayTask.Month, dayTask.Day, 23, 30, 0, 0);

            double tickTimeSM = (double)(startMorning   - DateTime.Now).TotalMilliseconds;
            double tickTimeEM = (double)(endMorning     - DateTime.Now).TotalMilliseconds;
            double tickTimeSA = (double)(startAfternoon - DateTime.Now).TotalMilliseconds;
            double tickTimeEA = (double)(endaftArnoon   - DateTime.Now).TotalMilliseconds;
            double tickTimeTR = (double)(lastTask - DateTime.Now).TotalMilliseconds;

            var timerSM = new Timer(tickTimeSM);
            var timerEM = new Timer(tickTimeEM);
            var timerSA = new Timer(tickTimeSM);
            var timerEA = new Timer(tickTimeEM);
            var timerTR = new Timer(tickTimeTR);

            timerSM.Elapsed += new ElapsedEventHandler(UpdateReservation);
            timerEM.Elapsed += new ElapsedEventHandler(UpdateReservation);
            timerSA.Elapsed += new ElapsedEventHandler(UpdateReservation);
            timerEA.Elapsed += new ElapsedEventHandler(UpdateReservation);
            timerTR.Elapsed += new ElapsedEventHandler(LastJob);

            timerSM.Start();
            timerEM.Start();
            timerSA.Start();
            timerEA.Start();
            timerTR.Start();
        }

        private void LastJob(object sender, ElapsedEventArgs e)
            => InitTasks(DateTime.Now.AddDays(1));

        private void UpdateReservation(object sender, ElapsedEventArgs e)
        {
            HashSet<Reservation.Statuses> validStatuses = //O(1) contains resolution
                new HashSet<Reservation.Statuses>
                    { Reservation.Statuses.ATTIVA, Reservation.Statuses.CHECKIN, Reservation.Statuses.INCORSO };

            var now = DateTime.UtcNow.ToString("o");

            var qres = from res in _reservationRepo.AsQueryable()
                       where validStatuses.Contains(res.Status)
                       select res;

            if (qres == null)
                return;

            foreach(var res in qres)
            {
                if (string.Compare(res.StartTime, now) < 0) //Not yet started
                {
                    res.Status = Reservation.Statuses.ATTIVA;
                }
                else //Others
                {
                    if (string.Compare(res.ExitTime, now) < 0) //Expired
                    { 
                        res.Status = Reservation.Statuses.CONCLUSA;
                        var wks = _workSpaceRepo.Find<WorkSpace>(x => x.Id == res.Target).FirstOrDefault();
                        _workSpaceRepo.ReplaceOne<WorkSpace>(x => x.Id == wks.Id, wks);
                    }
                    else //CHECKIN or INCORSO
                    { 
                        res.Status = 
                        res.Status == Reservation.Statuses.CHECKIN ? 
                        Reservation.Statuses.CHECKIN : Reservation.Statuses.INCORSO;  
                    }
                }
                _reservationRepo.ReplaceOne<Reservation>(dbres => res.Id == dbres.Id, res);
            }
        }
    }
}
