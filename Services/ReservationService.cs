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
        /// <summary>
        /// Required external aggregation repository _workSpaceRepo is the WorkSpace collection repo
        /// Main used in <see cref="WorkSpaceService"/>
        /// </summary>
        private readonly IMongoCollection<WorkSpace> _workSpaceRepo;
        /// <summary>
        /// Required external aggregation repository _workSpaceReservations is the WorkSpaceReservations collection repo
        /// In this repo are stored the Workspace by date reservations collected is also a main repo of this service.
        /// </summary>
        private readonly IMongoCollection<WorkSpaceReservation> _workSpaceReservationRepo;
        //@TODO use settings or database
        private readonly HashSet<Reservation.Statuses> _goingStatuses = new HashSet<Reservation.Statuses>
        { Reservation.Statuses.ATTIVA, Reservation.Statuses.CHECKIN, Reservation.Statuses.INCORSO };
        private readonly HashSet<Reservation.Statuses> _storedStatuses = new HashSet<Reservation.Statuses>
        { Reservation.Statuses.CANCELLATA, Reservation.Statuses.CONCLUSA };
        /// <summary>
        /// Constructor gets the mongo settings from DI
        /// </summary>
        /// <param name="settings">The mongo database settings</param>
        public ReservationService(IRoomServiceMongoSettings settings)
        {
            //Init base class
            base.Init(settings, settings.ReservationCollection);
            //Connect to required extra collections
            _workSpaceRepo = Database.GetCollection<WorkSpace>(settings.WorkSpaceCollection);
            _workSpaceReservationRepo = Database.GetCollection<WorkSpaceReservation>(settings.WorkSpaceReservationCollection);
        }
        /// <summary>
        /// Return all reservations of chosen user
        /// </summary>
        /// <param name="id">Ref user id</param>
        /// <returns>IEnumerable of reservations of selected user</returns>
        public IEnumerable<Reservation> GetUserReservations(string id)
            => Collection.Find(res => res.Owner == id).ToEnumerable();
        /// <summary>
        /// Execute a join of 3 tables for return util data
        /// Uses the reservation as "middleman" for join workspace and availability in reservation interval
        /// </summary>
        /// <param name="id">The user Id</param>
        /// <returns>Joined reservation workspace and availability in the interval DeltaTine of the requested user reservations</returns>
        public IEnumerable<WorkSpaceReservationDTO> GetUserReservationsAndWorkSpaces(string id)
        {
            //Get user reservation Note: in mongo query from could be called in only one mongo collection
            var reservations = Collection.Find(res => res.Owner == id).ToEnumerable();
            //Start the query
            var qres = from res in reservations.AsQueryable() //From reservation
                       join workspace in _workSpaceRepo.AsQueryable() on res.Target equals workspace.Id //Join workspace using id
                       join workres in _workSpaceReservationRepo.AsQueryable() on res.ReservationSocket equals workres.Id //join workspaceReservations using id
                       select new WorkSpaceReservationDTO //Aggregate result class
                       {
                           Users = workspace.AllSeats - workres.Reservations,
                           Interval = res.Interval,
                           WorkSpace = workspace
                       };
            //Return enumerable result
            return qres.AsEnumerable();
        }
        /// <summary>
        /// Find Reservation with ongoing status for an user
        /// </summary>
        /// <param name="WorkSpaceId">The id of the workspace</param>
        /// <param name="UserId">The user to check</param>
        /// <returns>nullable string of eventually found reservation</returns>
        public string FindOnGoindReservationIdByWorkSpaceAndUserIds(string WorkSpaceId, string UserId)
            =>
                Collection.Find
                (
                    res => res.Target == WorkSpaceId && 
                    res.Owner == UserId &&
                    res.Status == Reservation.Statuses.INCORSO || res.Status == Reservation.Statuses.CHECKIN
                ).FirstOrDefault()?.Id;
        /// <summary>
        /// Create a reservation
        /// Could chain a reservation time split
        /// </summary>
        /// <param name="model">The reservation model to add</param>
        /// <returns>Last created reservation or empty if fails</returns>
        public override Reservation Create(Reservation model)
        {
            //Clamp reservation interval inside valid times
            model.Interval = model.Interval.Clamp();
            //Invalidated after clamp error
            if (!model.Interval.IsValid())
                return new Reservation();
            //Get last reservation interval
            var EndTime = DateTime.Parse(model.Interval.EndTime, null, DateTimeStyles.RoundtripKind);
            //Create a candidate building
            var Buffer = new List<Reservation>();
            //Clone the model for iterator
            var Iterator = model.Clone();
            //Iterate first interval
            Iterator.Interval = Iterator.Interval.First();
            //If model is valid ad a copy to add buffer
            if (this.CanCreateReservation(Iterator))
                Buffer.Add(Iterator.Clone());
            else
                return new Reservation(); //eror Invalid model
            //Iterate reservation splits
            while ((Iterator.Interval = Iterator.Interval.Next(EndTime)) != null) 
            {
                //Valid submodel add a copy to buffer
                if (this.CanCreateReservation(Iterator))
                    Buffer.Add(Iterator.Clone());
                else
                    return new Reservation(); //Eroor Invalid submodel
            }
            //Get last item
            Reservation last = null;
            //Create a copy of buffer elements in the database and set last
            foreach (var res in Buffer)
                last = CreationHelper(res);
            //Return last created reservation from cretion buffer
            if (last != null)
                return last;
            //Error? Northing added
            return new Reservation();
        }
        /// <summary>
        /// Helper class for reservation creation
        /// </summary>
        /// <param name="model">The new reservation Template</param>
        /// <returns>The created Reservation</returns>
        private Reservation CreationHelper(Reservation model)
        {
            //Insert the validated model
            Collection.InsertOne(model);
            //Get target workspace reservations in time interval
            WorkSpaceReservation target;
            //Query
            var qres = from wsr in _workSpaceReservationRepo.AsQueryable()
                       where wsr.Owner == model.Target && wsr.Interval.Equals(model.Interval)
                       select wsr;
            //Create new workspace reservations in time interval if not found
            if ((target = qres.FirstOrDefault()) == null)
                target = new WorkSpaceReservation
                {
                    Owner = model.Target,
                    Interval = model.Interval,
                    Reservations = 0
                };
            //Add reservation counter
            target.Reservations++;
            //No target in the database -> add else update
            if (target.Id != null)
                _workSpaceReservationRepo.ReplaceOne<WorkSpaceReservation>(_wsr => _wsr.Id == target.Id, target);
            else _workSpaceReservationRepo.InsertOne(target);
            //Update model refs
            model.ReservationSocket = target.Id;
            Update(model.Id, model);
            //Return created model in database
            return model;
        }
        /// <summary>
        /// Base delete override for ref cascade deletion
        /// </summary>
        /// <param name="id">The target reservation id to delete</param>
        /// <returns>Mongo delete result instance</returns>
        public override DeleteResult Delete(string id)
        {
            //Get the item to delete
            var item = Read(id);
            //Get reservation targeted workspace seat
            var target = _workSpaceReservationRepo.Find<WorkSpaceReservation>(tar => tar.Id == item.ReservationSocket).FirstOrDefault();
            //Target error
            if (target == null)
                return base.Delete(id);
            //Remove reservation seat
            target.Reservations--;
            //Update on database
            _workSpaceReservationRepo.ReplaceOne<WorkSpaceReservation>(_wsr => _wsr.Id == target.Id, target);
            //Delete
            return base.Delete(id);
        }
        /// <summary>
        /// Execute a check-in add
        /// </summary>
        /// <param name="id">The reservation id</param>
        /// <param name="data">The timestamp of check-in</param>
        /// <returns>Bool : true successful checked-in false : else</returns>
        public bool CheckIn(string id, WorkSpaceDateDTO data)
        {
            //@TODO use external validator?
            //Not check-in valid status or already checked-in or not running
            var reservation = Read(id);
            if  (_storedStatuses.Contains(reservation.Status) 
                || reservation.Status == Reservation.Statuses.CHECKIN 
                || reservation.Status == Reservation.Statuses.ATTIVA
                )
                return false;
            //Add check-in
            reservation.CheckIn = reservation.CheckIn.Append(data.Date);
            //Switch statis
            reservation.Status = Reservation.Statuses.CHECKIN;
            //Return update result
            return Update(id, reservation).IsAcknowledged;
        }
        /// <summary>
        /// Execute a check-out add
        /// </summary>
        /// <param name="id">The reservation id</param>
        /// <param name="data">The timestamp of checkout</param>
        /// <returns>Bool : true successful checked-out false : else</returns>
        public bool CheckOut(string id, WorkSpaceDateDTO data)
        {
            //Get target reservation
            var reservation = Read(id);
            //Not checked-in error
            if (reservation.Status != Reservation.Statuses.CHECKIN)
                return false;
            //Date inside prenotation
            reservation.CheckOut = reservation.CheckOut.Append(data.Date);
            //Switch status to on goind
            reservation.Status = Reservation.Statuses.INCORSO;
            //Return update status
            return Update(id, reservation).IsAcknowledged;
        }
        /// <summary>
        /// Get aggregated reservation target WorkSpace and WorkSpaceReservations in the same time Interval
        /// </summary>
        /// <param name="id">The reservation Id</param>
        /// <returns>Reservation Joined WorkSpace and UserCounts inside</returns>
        public WorkSpaceReservationDTO GetReservationMeta(string id)
        {
            var res = Read(id);
            if (res == null) return null;
            var workSpc = _workSpaceRepo.Find(workSpace => workSpace.Id == res.Target).FirstOrDefault();
            if (workSpc == null) return null;
            var workRes = _workSpaceReservationRepo.Find(workRes => workRes.Id == res.ReservationSocket).FirstOrDefault();
            if (workRes == null) return null;
            return new WorkSpaceReservationDTO { WorkSpace = workSpc, Interval = res.Interval, Users = workRes.Reservations };
        }
        /// <summary>
        /// Performs controls for Reservation insert availability
        /// @TODO: move into utility class
        /// @TODO: return specific error description
        /// @TODO: add null handling and safe code controls
        /// </summary>
        /// <param name="model">The model Reservation to check creation availability</param>
        /// <returns>Bool : true can create : false can't</returns>
        public bool CanCreateReservation(Reservation model)
        {
            //Invalid model
            if (!model.Interval.IsValid())
                return false;
            //Query gets all valid statuses reservations of user owner of the reservation
            var qres = from res in Collection.AsQueryable()
                       where model.Owner == res.Owner && _goingStatuses.Contains(res.Status)
                       select res;
            //Get target Workspace seat availability
            var wrk = _workSpaceRepo.Find<WorkSpace>(sp => sp.Id == model.Target).FirstOrDefault();
            var wsr = _workSpaceReservationRepo.Find<WorkSpaceReservation>(tar => tar.Id == model.ReservationSocket).FirstOrDefault();
            //No Workspace
            if(wrk == null)
                return false;
            //Valid data
            if (wsr != null)
                if (wrk.AllSeats <= wsr.Reservations)
                    return false;
            //New reservation should not intersect others
            foreach (var userRes in qres)
                if ( DataInsersects
                (
                    userRes.Interval.StartTime, userRes.Interval.EndTime, 
                    model.Interval.StartTime, model.Interval.EndTime
                )) return false;
            //Return success
            return true;
        }
        /// <summary>
        /// Determinate if two datetime intervals in isostring overlaps
        /// @TODO move into an utility class
        /// </summary>
        /// <param name="start">First datetime interval start</param>
        /// <param name="end">First datetime interval end</param>
        /// <param name="secStart">Second datetime interval start</param>
        /// <param name="secEnd">Second datetime interval end</param>
        /// <returns>True : data intersects false : else</returns>
        private bool DataInsersects(string start, string end, string secStart, string secEnd)
        => !(string.Compare(start, secStart) < 0 ?
            string.Compare(end, secStart) < 0 : string.Compare(secEnd, start) < 0);
    }
}
