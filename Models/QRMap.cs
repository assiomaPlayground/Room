using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Model
{
    public class QRMap : IModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Code { get; set; }
        public string Room { get; set; }
    }
}
