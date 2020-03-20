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
        /// <summary>
        /// Id <see cref="IModel.Id"/>
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Name of the workspace
        /// Un valued properties are simply ignored
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// WorkSpace feature enumerable
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> Features { get; set; }

        /// <summary>
        /// Maximum WorkSpace available seats
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        [BsonRequired]
        public int AllSeats { get; set; }
        /// <summary>
        /// SubMap url to svg used for map the area
        /// Un valued properties are simply ignored
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        [BsonRequired]
        public string SubMap { get; set; }

        /// <summary>
        /// Ref Building used for aggregate with building 
        /// <see cref="Building.Building"/>
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)] 
        public string Building { get; set; }

        /// <summary>
        /// Pivot of Point2d where to stick the svg in map
        /// <see cref="Point2d"/>
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)] 
        public Point2d Pivot { get; set; }

        /// <summary>
        /// Owner of the resource in this case the building where the workspace is
        /// <see cref="IOwnable"/>
        /// </summary>
        [BsonIgnore][JsonIgnore]
        public string Owner { get => Building; set => Building = value; }
    }
}
