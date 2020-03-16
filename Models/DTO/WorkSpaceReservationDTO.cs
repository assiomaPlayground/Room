using Newtonsoft.Json;
using RoomService.Models;
using RoomService.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{
    public class WorkSpaceReservationDTO
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public WorkSpace Room { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public DeltaTime Times { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public int Users { get; set; }
    }
}
