using Newtonsoft.Json;
using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{
    public class BuildingSpacesDTO
    {

        //Id
        public string Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        //Name
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        //Map
        public string Map { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        //Rooms
        public IEnumerable<WorkSpace> Rooms { get; set; }
    }
}
