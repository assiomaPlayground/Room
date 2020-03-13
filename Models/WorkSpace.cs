using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using RoomService.Models;
using RoomService.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Models
{
    /// <summary>
    /// Model for workspace data
    /// </summary>
    public class WorkSpace : IModel, IOwnable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<string> Features { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        [BsonRequired]
        public int AllSeats { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        [BsonRequired]
        public string SubMap { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Building { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Point2d Pivot { get; set; }
        [BsonIgnore][JsonIgnore]
        public string Owner { get => Building; }
    }
}
