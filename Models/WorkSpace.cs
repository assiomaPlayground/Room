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

        //Id
        public string Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        //Name
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        //ICollection
        //Features
        public ICollection<string> Features { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        [BsonRequired]

        //AllSeats
        public int AllSeats { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        [BsonRequired]

        //SubMap
        public string SubMap { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)] 

        //Building
        public string Building { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)] 

        //Point2d
        //Pivot
        public Point2d Pivot { get; set; }
        [BsonIgnore][JsonIgnore] 
        
        //Owner
        public string Owner { get => Building; }
    }
}
