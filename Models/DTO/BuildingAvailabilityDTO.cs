using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{
    public class BuildingAvailabilityDTO
    {
        public Building TargetBuilding { get; set; }
        public IEnumerable<WorkSpaceAvailabilityDTO> Available { get; set; }
    }
}
