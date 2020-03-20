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
        /// Id <see cref="IModel.Id"/>
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// The Building name
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The building Map
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Map { get; set; }
    }
}
