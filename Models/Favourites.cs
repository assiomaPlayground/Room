using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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
        public string Target { get; set; }
        public string Owner { get; set; }
        public bool Last { get; set; }
        public int UsageTimes { get; set; }

    }
}
