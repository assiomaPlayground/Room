using MongoDB.Driver;
using RoomService.DTO;
using RoomService.Models;
using RoomService.Models.Types;
using RoomService.Settings;
using RoomService.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        //@TODO use settings or database
        private readonly HashSet<Reservation.Statuses> _goingStatuses = new HashSet<Reservation.Statuses>
        { Reservation.Statuses.ATTIVA, Reservation.Statuses.CHECKIN, Reservation.Statuses.INCORSO };
        private readonly HashSet<Reservation.Statuses> _storedStatuses = new HashSet<Reservation.Statuses>
        { Reservation.Statuses.CANCELLATA, Reservation.Statuses.CONCLUSA };

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

        public string FindOnGoindReservationIdByWorkSpaceAndUserIds(string WorkSpaceId, string UserId)
            =>
                Collection.Find
                (
                    res => res.Target == WorkSpaceId && 
                    res.Owner == UserId && 
                    _goingStatuses.Contains(res.Status)
                ).FirstOrDefault()?.Id;
        public override Reservation Create(Reservation model)
        {
            model.Interval = model.Interval.Clamp();
            if (!model.Interval.IsValid()) 
                return new Reservation();
            var Buffer = new List<Reservation>();

            var Iterator = model.Clone();
            Iterator.Interval = Iterator.Interval.First();
            
            if (this.CanCreateReservation(Iterator))
                Buffer.Add(Iterator.Clone());
            else
                return new Reservation();

            Iterator.Interval.EndTime = model.Interval.EndTime;
            
            while ((Iterator.Interval = Iterator.Interval.Next()) != null) 
            {
                if (this.CanCreateReservation(Iterator))
                    Buffer.Add(Iterator.Clone());
                else
                    return new Reservation();

                Iterator.Interval.EndTime = model.Interval.EndTime;
            }

            Reservation last = null;

            foreach (var res in Buffer)
                last = CreationHelper(res);

            if (last != null)
                return last;

            return new Reservation();
        }
        private Reservation CreationHelper(Reservation model)
        {
            Collection.InsertOne(model);

            WorkSpaceReservations target;

            var qres = from wsr in _workSpaceReservations.AsQueryable()
                       where wsr.Owner == model.Target && wsr.Times.Equals(model.Interval)
                       select wsr;

            if ((target = qres.FirstOrDefault()) == null)
                target = new WorkSpaceReservations
                {
                    Owner = model.Target,
                    Times = model.Interval,
                    Reservations = new List<string>()
                };

            if (!target.Reservations.Contains(model.Id)) //No duplicates
            {
                target.Reservations = target.Reservations.Append(model.Id);

                var workspace = _workSpaceRepo.Find(wrs => wrs.Id == target.Owner).FirstOrDefault();
                if (workspace == null) //error room not exists
                    return new Reservation();
                if (workspace.AllSeats < target.Reservations.Count()) //Room is full
                    return new Reservation();
            }
            else return new Reservation(); //Failed to add (Already exists)

            if (target.Id != null)
                _workSpaceReservations.ReplaceOne<WorkSpaceReservations>(_wsr => _wsr.Id == target.Id, target);
            else _workSpaceReservations.InsertOne(target);

            return model;
        }
        public override DeleteResult Delete(string id)
        {
            var item = Read(id);

            var qres = from wsr in _workSpaceReservations.AsQueryable()
                       where wsr.Owner == item.Target && wsr.Times.Equals(item.Interval)
                       select wsr;

            WorkSpaceReservations target;

            if ((target = qres.FirstOrDefault()) == null)
                return base.Delete(id);

            var edit = target.Reservations.ToHashSet();
            edit.Remove(id);
            target.Reservations = edit.AsEnumerable();
            _workSpaceReservations.ReplaceOne<WorkSpaceReservations>(_wsr => _wsr.Id == target.Id, target);
            return base.Delete(id);
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
            if (string.Compare(reservation.Interval.StartTime, data.Date) > 0)
                return false;
            if (string.Compare(reservation.Interval.EndTime, data.Date) < 0)
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
            if (string.Compare(reservation.Interval.StartTime, data.Date) > 0)
                return false;
            //Date inside prenotation
            reservation.CheckOut = reservation.CheckOut.Append(data.Date);
            reservation.Status = Reservation.Statuses.INCORSO;
            return Update(id, reservation).IsAcknowledged;
        }
        public WorkSpaceReservationDTO GetReservationByDeltaTimeAdWorkSpaceId(string id, DeltaTime deltaTime)
        {
            var res = _workSpaceReservations.Find(wsr => wsr.Owner == id && wsr.Times.Equals(deltaTime)).FirstOrDefault();
            if (res == null)
                return null;
            return new WorkSpaceReservationDTO
            {
                Room = _workSpaceRepo.Find<WorkSpace>(wrk => wrk.Id == id).FirstOrDefault(),
                Times = res.Times,
                Users = res.Reservations.Count()
            };
        }
        public bool CanCreateReservation(Reservation model)
        {
            if (!model.Interval.IsValid()) //Invalid
                return false;

            HashSet<Reservation.Statuses> filter = new HashSet<Reservation.Statuses>
            { Reservation.Statuses.ATTIVA, Reservation.Statuses.CHECKIN, Reservation.Statuses.INCORSO };
            var qres = from res in Collection.AsQueryable()
                       where model.Owner == res.Owner && filter.Contains(res.Status)
                       select res;

            var wsr = GetReservationByDeltaTimeAdWorkSpaceId(model.Target, model.Interval);
            if (wsr != null)
                if (wsr.Room.AllSeats <= wsr.Users)
                    return false;

            foreach (var userRes in qres)
                if (DataInsersects(userRes.Interval.StartTime, userRes.Interval.EndTime, model.Interval.StartTime, model.Interval.EndTime))
                    return false;

            return true;
        }
        private bool DataInsersects(string start, string end, string secStart, string secEnd)
        => !(string.Compare(start, secStart) < 0 ?
            string.Compare(end, secStart) < 0 : string.Compare(secEnd, start) < 0);
    }
}
