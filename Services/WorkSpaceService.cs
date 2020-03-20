using MongoDB.Driver;
using RoomService.Models;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Services
{
    /// <summary>
    /// Service for WorSpace collection crud ops in abstract
    /// </summary>
    public class WorkSpaceService : AbstractMongoCrudService<WorkSpace>
    {
        /// <summary>
        /// Needed repository for aggregation operations with Reservation
        /// </summary>
        private readonly IMongoCollection<Reservation> _reservationRepo;
        /// <summary>
        /// Needed repository for aggregation Workspace reservation repository for time reservations on a workspace
        /// </summary>
        private readonly IMongoCollection<WorkSpaceReservation> _workSpaceReservationRepo;
        /// <summary>
        /// Constructor sets the DI
        /// </summary>
        /// <param name="settings">The required settings</param>
        public WorkSpaceService(IRoomServiceMongoSettings settings)
        {
            //Base init
            base.Init(settings, settings.WorkSpaceCollection);
            //Get collections
            _reservationRepo = Database.GetCollection<Reservation>(settings.ReservationCollection);
            _workSpaceReservationRepo = Database.GetCollection<WorkSpaceReservation>(settings.WorkSpaceReservationCollection);
        }
        /// <summary>
        /// Get deleted by building Id
        /// </summary>
        /// <param name="id">the building Id</param>
        /// <returns>Delete result instance in mongo driver</returns>
        public DeleteResult DeleteByBuildingId(string id)
        {
            var targets = Collection.Find(workSpace => workSpace.Building == id).ToList();
            foreach (var work in targets)
                FreeWorkSpace(work.Id);
            return Collection.DeleteMany(workSpace => workSpace.Building == id);
        }
        /// <summary>
        /// Delete a workspace
        /// Chains a cascade delete of workspace reservation e reservation targeting the workspace
        /// </summary>
        /// <param name="id">The target Id</param>
        /// <returns>Delete result instance in mongo driver</returns>
        public override DeleteResult Delete(string id)
        {
            DeleteWorkSpaceReservationByWorkSpaceId(id);
            DeleteReservationByWorkSpaceId(id);
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
