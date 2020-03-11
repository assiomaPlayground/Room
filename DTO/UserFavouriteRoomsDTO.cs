using Newtonsoft.Json;
using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{
    public class UserFavouriteRoomsDTO
    {
        [Serializable]
        public class FavouriteRoom
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Include)]
            public WorkSpace Workspace { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Include)]
            public bool Last { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Include)]
            public int UsageTimes { get; set; }
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public UserModel Owner { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<FavouriteRoom> Rooms { get; set; }
    }
}
