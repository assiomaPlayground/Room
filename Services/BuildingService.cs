using MongoDB.Driver;
using RoomService.DTO;
using RoomService.Models;
using RoomService.Settings;
using System;
using System.Collections.Generic;
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
        /// Join repository
        /// </summary>
        private readonly WorkSpaceService workSpaceService;

        private readonly IMongoCollection<WorkSpaceReservations> _workSpaceReservationsRepo;
        public BuildingService(IRoomServiceMongoSettings settings, WorkSpaceService workSpaceService)
        {
            base.Init(settings, settings.BuildingCollection);
            this.workSpaceService = workSpaceService;
            this._workSpaceReservationsRepo = Database.GetCollection<WorkSpaceReservations>(settings.ReservationCollection);
        }
        /// <summary>
        /// Join Building RoomsId with workspace data
        /// </summary>
        /// <param name="id">The building id</param>
        /// <returns>BuildingSpacesDTO the building and his spaces data</returns>
        public BuildingSpacesDTO GetBuildingSpaces(string id)
        {
            var building = Read(id);
            var qres =
                from space in workSpaceService.Collection.AsQueryable()
                where building.Rooms.Contains(space.Id)
                select space;
            return new BuildingSpacesDTO { Id = building.Id, Map = building.Map, Name = building.Name, Rooms = qres.ToArray() };
        }

        public BuildingAvailabilityDTO GetAvailableBuildingSpaces(string id, string start, string end)
        {
            var building = Read(id); if (building == null) return null;
            var qres =
                from space in workSpaceService.Collection.AsQueryable()
                where building.Rooms.Contains(space.Id)
                join res in _workSpaceReservationsRepo.AsQueryable() on space.Id equals res.Owner
                into WorkSpaces from ews in WorkSpaces.DefaultIfEmpty()
                where ews.Reservations.Count() < space.AllSeats
                select new WorkSpaceAvailabilityDTO{ 
                    TargetWorkSpace = space,
                    Availability = ews == null ? space.AllSeats : space.AllSeats - ews.Reservations.Count()
                };
            return new BuildingAvailabilityDTO
            {
                TargetBuilding = building,
                Available = qres.ToArray()
            };
        }

        public override DeleteResult Delete(string id)
        {
            workSpaceService.DeleteByBuildingId(id);//Cascade delete rooms in building
            return base.Delete(id);
        }
    }
}