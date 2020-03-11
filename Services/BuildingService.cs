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
        public BuildingService(IRoomServiceMongoSettings settings, WorkSpaceService workSpaceService)
        {
            base.Init(settings, settings.BuildingCollection);
            this.workSpaceService = workSpaceService;
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
                select new WorkSpace
                {
                    Id = space.Id,
                    AllSeats = space.AllSeats,
                    Building = space.Building,
                    Features = space.Features,
                    Name = space.Name,
                    Pivot = space.Pivot,
                    Seats = space.Seats,
                    SubMap = space.SubMap
                };
            return new BuildingSpacesDTO { Id = building.Id, Map = building.Map, Name = building.Name, Rooms = qres.ToArray() };
        }

        public override DeleteResult Delete(string id)
        {
            workSpaceService.DeleteByBuildingId(id);//Cascade delete rooms in building
            return base.Delete(id);
        }
    }
}