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
        private readonly IMongoCollection<Reservation> _reservationRepository;
        /// <summary>
        /// Constructor sets the DI
        /// </summary>
        /// <param name="settings">The required settings</param>
        public WorkSpaceService(IRoomServiceMongoSettings settings)
        {
            base.Init(settings, settings.WorkSpaceCollection);
            _reservationRepository = Database.GetCollection<Reservation>(settings.ReservationCollection);
        }
        /// <summary>
        /// Get deleted by building ID
        /// </summary>
        /// <param name="id">the building Id</param>
        /// <returns>Delete result instance in mongo driver</returns>
        public DeleteResult DeleteByBuildingId(string id)
        {
            var targets = Collection.Find(room => room.Building == id).ToList();
            foreach (var work in targets)
                DeleteReservationsByRoomId(work.Id);
            return Collection.DeleteMany(room => room.Building == id);
        }
        /// <summary>
        /// Delete a workspace
        /// </summary>
        /// <param name="id">The target Id</param>
        /// <returns>Delete result instance in mongo driver</returns>
        public override DeleteResult Delete(string id)
        {
            DeleteReservationsByRoomId(id);
            return base.Delete(id);
        }
        /// <summary>
        /// Delete reservations of targeted room
        /// </summary>
        /// <param name="id">The target room Id</param>
        /// <returns>Delete result instance in mongo driver</returns>
        public DeleteResult DeleteReservationsByRoomId(string id)
            => _reservationRepository.DeleteMany(res => res.Target == id);
    }
}
