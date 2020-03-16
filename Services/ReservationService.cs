using MongoDB.Driver;
using RoomService.DTO;
using RoomService.Models;
using RoomService.Models.Types;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Services
{
    /// <summary>
    /// Service for Reservation collection crud ops in abstract
    /// </summary>
    public class ReservationService : AbstractMongoCrudService<Reservation>
    {
        private readonly IMongoCollection<WorkSpace> _workSpaceRepo;
        private readonly IMongoCollection<WorkSpaceReservations> _workSpaceReservations;
        public ReservationService(IRoomServiceMongoSettings settings)
        {
            base.Init(settings, settings.ReservationCollection);

            _workSpaceRepo = Database.GetCollection<WorkSpace>(settings.WorkSpaceCollection);
            _workSpaceReservations = Database.GetCollection<WorkSpaceReservations>(settings.WorkSpaceReservationCollection);
        }
        public DeleteResult DeleteByUserId(string id)
            => Collection.DeleteMany(res => res.Owner == id);
        public DeleteResult DeleteByRoomId(string id)
            => Collection.DeleteMany(res => res.Target == id);
        public IEnumerable<Reservation> GetUserReservations(string id)
            => Collection.Find(res => res.Owner == id).ToEnumerable();

        public override void Create(Reservation model)
        {
            Collection.InsertOne(model);

            WorkSpaceReservations target;

            var qres = from wsr in _workSpaceReservations.AsQueryable()
                       where wsr.Owner == model.Target && wsr.Times.Equals(model.Day)
                       select wsr;

            if ((target = qres.FirstOrDefault()) == null)
                target = new WorkSpaceReservations
                {
                    Owner = model.Target,
                    Times = model.Day,
                    Reservations = new List<string>()
                };

            if (!target.Reservations.Contains(model.Id)) //No duplicates
            {
                target.Reservations = target.Reservations.Append(model.Id);

                var workspace = _workSpaceRepo.Find(wrs => wrs.Id == target.Owner).FirstOrDefault();
                if (workspace == null) //error room not exists
                    return;
                if (workspace.AllSeats < target.Reservations.Count()) //Room is full
                    return;
            }
            else return; //Failed to add (Already exists)

            if (target.Id != null)
                _workSpaceReservations.ReplaceOne<WorkSpaceReservations>(_wsr => _wsr.Id == target.Id, target);
            else _workSpaceReservations.InsertOne(target);
        }

        public bool CheckIn(string id, WorkSpaceDateDTO data)
        {
            //@TODO use external validator
            var reservation = Read(id);
            if (reservation.Status == Reservation.Statuses.CANCELLATA ||
                reservation.Status == Reservation.Statuses.CONCLUSA ||
                reservation.Status == Reservation.Statuses.CHECKIN
            )
                return false;
            //Valid status
            if (string.Compare(reservation.Day.StartTime, data.Date) > 0)
                return false;
            if (string.Compare(reservation.Day.EndTime, data.Date) < 0)
                return false;
            //Date inside prenotation
            reservation.CheckIn = reservation.CheckIn.Append(data.Date);
            reservation.Status = Reservation.Statuses.CHECKIN;
            return Update(id, reservation).IsAcknowledged;
        }
        public bool CheckOut(string id, WorkSpaceDateDTO data)
        {
            var reservation = Read(id);
            if (reservation.Status != Reservation.Statuses.CHECKIN)
                return false;
            if (string.Compare(reservation.Day.StartTime, data.Date) > 0)
                return false;
            //Date inside prenotation
            reservation.CheckOut = reservation.CheckOut.Append(data.Date);
            reservation.Status = Reservation.Statuses.INCORSO;
            return Update(id, reservation).IsAcknowledged;
        }

        public WorkSpaceReservationDTO GetReservationByDeltaTimeAdWorkSpaceId(string id, DeltaTime deltaTime)
        {
            var res = _workSpaceReservations.Find(wsr => wsr.Owner == id && wsr.Times.Equals(deltaTime)).FirstOrDefault();
            return new WorkSpaceReservationDTO
            {
                Room = _workSpaceRepo.Find<WorkSpace>(wrk => wrk.Id == id).FirstOrDefault(),
                Times = res.Times,
                Users = res.Reservations.Count()
            };
        }
    }
}
