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
        private readonly ReservationService _reservationService;
        public WorkSpaceService(
            IRoomServiceMongoSettings settings, 
            ReservationService reservationService
        )
        {
            base.Init(settings, settings.WorkSpaceCollection);
            _reservationService = reservationService;
        }
        public DeleteResult DeleteByBuildingId(string id)
        {
            var targets = Collection.Find(room => room.Building == id).ToList();
            foreach (var work in targets)
                _reservationService.DeleteByRoomId(work.Id);
            return Collection.DeleteMany(room => room.Building == id);
        }
        public override DeleteResult Delete(string id)
        {
            _reservationService.DeleteByRoomId(id);
            return base.Delete(id);
        }
    }
}
