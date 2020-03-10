using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Model
{
    /// <summary>
    /// Model for building data
    /// </summary>
    public class Building : IModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Map { get; set; }
        public ICollection<string> Rooms { get; set; }
    }
}
