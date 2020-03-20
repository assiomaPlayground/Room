using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomService.Models;

namespace RoomService.DTO
{
    /// <summary>
    /// User Favourite workSpace aggregation
    /// <see cref="UserModel"/>
    /// <see cref="Favourites"/>
    /// <seealso cref="WorkSpace"/>
    /// </summary>
    public class UserFavouriteWorkSpaceDTO
    {
        /// <summary>
        /// Subclass for favourite collection
        /// </summary>
        public class FavouriteWorkSpaceDTO
        {
            /// <summary>
            /// The favourite
            /// </summary>
            public Favourites Favourite { get; set; }
            /// <summary>
            /// The WorkSpace
            /// </summary>
            public WorkSpace WorkSpace { get; set; }
        }
        /// <summary>
        /// The user
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public UserModel User { get; set; }
        /// <summary>
        /// Favourites workSpaces
        /// </summary>
        public IEnumerable<FavouriteWorkSpaceDTO> Favourites { get; set; }
    }
}
