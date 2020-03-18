using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Models
{
    /// <summary>
    /// Model for building data
    /// </summary>
    public class Building : IModel
    {

        /// <summary>
        /// Id
        /// Un valued properties are simply ignored
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
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
        public ICollection<string> Rooms { get; set; }
    }
}
