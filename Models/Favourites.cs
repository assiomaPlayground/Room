using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Models
{
    /// <summary>
    /// Model for Favourites data
    /// </summary>
    public class Favourites : IModel, IOwnable
    {
        /// <summary>
        /// Id <see cref="IModel.Id"/>
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Target resource favourited in this case refers WorkSpace
        /// <see cref="WorkSpace"/>
        /// </summary>
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string Target { get; set; }

        /// <summary>
        /// Owner of the Favourite in this case the User
        /// <see cref="UserModel"/>
        /// <seealso cref="IOwnable"/>
        /// </summary>
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string Owner { get; set; }

        /// <summary>
        /// Bool indicating if this liked was last reserved WorkSpace by owner
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)] 
        public bool Last { get; set; }

        /// <summary>
        /// Times the Favourite were used 
        /// Prop for favoutirs sorting
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public int UsageTimes { get; set; }
    }
}
