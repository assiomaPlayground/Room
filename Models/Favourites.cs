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
        /// Id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]

        /// <summary>
        /// Target
        /// </summary>
        public string Target { get; set; }
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]

        /// <summary>
        /// Owner
        /// </summary>
        public string Owner { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)] 

        /// <summary>
        /// Last
        /// </summary>
        public bool Last { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]

        /// <summary>
        /// <param name = "UsageTimes"></param>
        /// </summary>
        public int UsageTimes { get; set; }
    }
}
