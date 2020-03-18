using Newtonsoft.Json;
using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{

    /// <summary>
    /// possibility of users to reserve a place in the environments to which access has been provided
    /// </summary>
    public class UserFavouriteRoomsDTO
    {
        [Serializable]
        public class FavouriteRoom
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Include)]

            /// <summary>
            /// Workspace
            /// </summary>
            public WorkSpace Workspace { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Include)]

            /// <summary>
            /// Last variable of the Boolean type
            /// </summary>
            public bool Last { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Include)]

            /// <summary>
            /// <param name ="UsageTimes"></param>
            /// </summary>
            public int UsageTimes { get; set; }
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]

        /// <summary>
        /// Owner 
        /// Un valued properties are simply ignored
        /// </summary>
        public UserModel Owner { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        /// <summary>
        /// Rooms
        /// </summary>
        public IEnumerable<FavouriteRoom> Rooms { get; set; }
    }
}
