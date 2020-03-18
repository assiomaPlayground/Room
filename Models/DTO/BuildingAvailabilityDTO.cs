using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{

    /// <summary>
    /// Availability of Building
    /// </summary>
    public class BuildingAvailabilityDTO
    {

        /// <summary>
        /// Target of Building
        /// </summary>
        public Building TargetBuilding { get; set; }
        public IEnumerable<WorkSpaceAvailabilityDTO> Available { get; set; }
    }
}
