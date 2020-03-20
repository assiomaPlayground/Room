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
    /// Service for Building collection crud ops in abstract
    /// </summary>
    public class BuildingService : AbstractMongoCrudService<Building>
    {
        /// <summary>
        /// Needed repository for aggregation operations with WorkSpace
        /// </summary>
        private readonly IMongoCollection<WorkSpace> _workSpaceRepo;
        /// <summary>
        /// Needed repository for aggregation operations with WorkSpaceReservation
        /// </summary>
        private readonly IMongoCollection<WorkSpaceReservation> _workSpaceReservationRepo;
        /// <summary>
        /// Needed repository for aggregation operations with Reservation
        /// </summary>
        private readonly IMongoCollection<Reservation> _reservationRepo;
        //@TODO use settings or database
        private readonly HashSet<Reservation.Statuses> _goingStatuses = new HashSet<Reservation.Statuses>
        { Reservation.Statuses.ATTIVA, Reservation.Statuses.CHECKIN, Reservation.Statuses.INCORSO };
        private readonly HashSet<Reservation.Statuses> _storedStatuses = new HashSet<Reservation.Statuses>
        { Reservation.Statuses.CANCELLATA, Reservation.Statuses.CONCLUSA };
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">The mongo settings wrapper</param>
        public BuildingService(IRoomServiceMongoSettings settings)
        {
            base.Init(settings, settings.BuildingCollection);

            this._workSpaceReservationRepo = Database.GetCollection<WorkSpaceReservation>(settings.ReservationCollection);
            this._workSpaceRepo = Database.GetCollection<WorkSpace>(settings.WorkSpaceCollection);
            this._reservationRepo = Database.GetCollection<Reservation>(settings.ReservationCollection);
        }
        /// <summary>
        /// Join Building and WorkSpaces
        /// </summary>
        /// <param name="id">The building id</param>
        /// <returns>BuildingWorkSpaceDTO the building and his WorkSpaces</returns>
        public BuildingWorkSpaceDTO GetBuildingWorkSpace(string id)
        {
            //Read
            var building = Read(id);
            //Query
            var qres =
                from space in _workSpaceRepo.AsQueryable()
                where space.Owner == id
                select space;
            //Return aggregated Data
            return new BuildingWorkSpaceDTO { Building = building, WorkSpaces = qres.ToArray() };
        }
        /// <summary>
        /// Find all available workspaces inside a building for an user in chosen DeltaTime interval
        /// </summary>
        /// <param name="rid">The user id</param>
        /// <param name="id">The building id</param>
        /// <param name="interval">The time interval</param>
        /// <returns>The building and a IEnumerable of found workspaces</returns>
        public BuildingWorkSpaceDTO GetAvailableBuildingWorkSpace(string rid, string id, DeltaTime interval)
        {
            //Building checks
            var building = Read(id); if (building == null) return null;
            //Create tester reservation
            var Tester = new Reservation { Interval = interval, Owner = rid };
            //Get all WorkSpaces in the building
            var qres =
               from workSpace in _workSpaceRepo.AsQueryable()
               where workSpace.Building == id
               select workSpace;
            //Found workspaces buffer
            var Buffer = new List<WorkSpace>();
            //Look for all reservations solutions
            foreach (var workSpace in qres)
            {
                Tester.Target = workSpace.Id;
                var Result = AvailabilityHeper(Tester);
                if (Result)
                    Buffer.Add(workSpace);
            }
            return new BuildingWorkSpaceDTO { Building = building, WorkSpaces = Buffer };
        }
        /// <summary>
        /// Helper class for WorkSpace availability Checks
        /// Use a reservation model as tester for simulate the reservation
        /// Logic is similar to <seealso cref="ReservationService.Create(Reservation)"/>
        /// </summary>
        /// <param name="model">Reservation model used as tester</param>
        /// <returns>Bool true : could create false : else</returns>
        private bool AvailabilityHeper(Reservation model)
        {
            //Clamp reservation interval inside valid times
            model.Interval = model.Interval.Clamp();
            //Invalidated after clamp error
            if (!model.Interval.IsValid())
                return false;
            //Get last reservation interval
            var EndTime = DateTime.Parse(model.Interval.EndTime, null, DateTimeStyles.RoundtripKind);
            //Setup iterator
            var Iterator = model.Clone();
            //Check first data
            Iterator.Interval = Iterator.Interval.First();
            //First test
            if (!this.CanCreateReservation(Iterator))
                return false;
            //Execute next tests until null
            while ((Iterator.Interval = Iterator.Interval.Next(EndTime)) != null)
                if (!this.CanCreateReservation(Iterator))
                    return false;
            //Success
            return true;
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
            var qres = from res in _reservationRepo.AsQueryable()
                       where model.Owner == res.Owner && _goingStatuses.Contains(res.Status)
                       select res;
            //Get target Workspace seat availability
            var wrk = _workSpaceRepo.Find<WorkSpace>(sp => sp.Id == model.Target).FirstOrDefault();
            var wsr = _workSpaceReservationRepo.Find<WorkSpaceReservation>(tar => tar.Id == model.ReservationSocket).FirstOrDefault();
            //No Workspace
            if (wrk == null)
                return false;
            //Valid data
            if (wsr != null)
                if (wrk.AllSeats <= wsr.Reservations)
                    return false;
            //New reservation should not intersect others
            foreach (var userRes in qres)
                if (DataInsersects
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
        /// <summary>
        /// Delete building chains a delete of all workspace inside it all reservations inside workspaces and 
        /// all workspacereservations of the workspace
        /// </summary>
        /// <param name="id">The building id</param>
        /// <returns>Delete result</returns>
        public override DeleteResult Delete(string id)
        {
            var targets = _workSpaceRepo.Find(workSpace => workSpace.Building == id).ToList();
            foreach (var work in targets)
                FreeWorkSpace(work.Id);
            _workSpaceRepo.DeleteMany(workSpace => workSpace.Building == id);
            return base.Delete(id);
        }
        /// <summary>
        /// Delete all refs Reservation and WorkSpaceReservation from the workspace
        /// </summary>
        /// <param name="id">The target WorkSpace</param>
        /// <returns>Delete result instance in mongo driver</returns>
        public DeleteResult FreeWorkSpace(string id)
        {
            DeleteWorkSpaceReservationByWorkSpaceId(id);
            return DeleteReservationByWorkSpaceId(id);
        }
        /// <summary>
        /// Delete reservations of targeted WorkSpace
        /// </summary>
        /// <param name="id">The target WorkSpace Id</param>
        /// <returns>Delete result instance in mongo driver</returns>
        private DeleteResult DeleteWorkSpaceReservationByWorkSpaceId(string id)
            => _workSpaceReservationRepo.DeleteMany(res => res.Owner == id);
        /// <summary>
        /// Delete reservations of targeted WorkSpace
        /// </summary>
        /// <param name="id">The target WorkSpace Id</param>
        /// <returns>Delete result instance in mongo driver</returns>
        private DeleteResult DeleteReservationByWorkSpaceId(string id)
            => _reservationRepo.DeleteMany(res => res.Target == id);
    }
}