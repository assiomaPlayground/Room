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
    /// Model for favourites data
    /// </summary>
    public class Favourites : IModel, IOwnable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string Target { get; set; }
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string Owner { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public bool Last { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public int UsageTimes { get; set; }
    }
}
