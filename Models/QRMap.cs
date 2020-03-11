using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Models
{
    /// <summary>
    /// Model for QRMap data
    /// </summary>
    public class QRMap : IModel, IOwnable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Code { get; set; }
        public string Room { get; set; }
        [BsonIgnore]
        public string Owner { get => Room; }
    }
}
