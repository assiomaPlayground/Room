using Newtonsoft.Json;
using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{

    /// <summary>
    /// Possibility to choose between various spaces and buildings
    /// </summary>
    public class BuildingSpacesDTO
    {

        /// <summary>
        /// Id of type string
        /// Un valued properties are simply ignored
        /// </summary>
        public string Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        /// <summary>
        /// Name
        /// Un valued properties are simply ignored
        /// </summary>

        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        /// <summary>
        /// Map
        /// Un valued properties are simply ignored
        /// </summary>
        public string Map { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        /// <summary>
        /// Rooms
        /// </summary>
        public IEnumerable<WorkSpace> Rooms { get; set; }
    }
}
