using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using RoomService.Models;
using RoomService.Models.Types;
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

        /// <summary>
        /// Id
        /// Un valued properties are simply ignored
        /// </summary>
        public string Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        /// <summary>
        /// Name
        /// Un valued properties are simply ignored
        /// </summary>
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        /// <summary>
        /// Features of ICollection
        /// </summary>
        public ICollection<string> Features { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        [BsonRequired]

        /// <summary>
        /// AllSeats
        /// </summary>
        public int AllSeats { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        [BsonRequired]

        /// <summary>
        /// SubMap
        /// Un valued properties are simply ignored
        /// </summary>
        public string SubMap { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)] 

        //Building
        public string Building { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)] 

        /// <summary>
        /// Pivot of Point2d
        /// </summary>
        public Point2d Pivot { get; set; }
        [BsonIgnore][JsonIgnore] 
        
        /// <summary>
        /// Owner
        /// </summary>
        public string Owner { get => Building; }
    }
}
