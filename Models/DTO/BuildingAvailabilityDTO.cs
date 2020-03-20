using Newtonsoft.Json;
using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{
    /// <summary>
    /// Availability of Building DTO
    /// </summary>
    public class BuildingAvailabilityDTO
    {
        /// <summary>
        /// Target Building
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Building TargetBuilding { get; set; }
        /// <summary>
        /// Available WorkSpaces
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public IEnumerable<WorkSpaceReservationDTO> Available { get; set; }
    }
}
