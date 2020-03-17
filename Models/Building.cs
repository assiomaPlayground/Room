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

        //Id
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)] 

        //Name
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        //Map
        public string Map { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        //ICollection
        //Rooms
        public ICollection<string> Rooms { get; set; }
    }
}
