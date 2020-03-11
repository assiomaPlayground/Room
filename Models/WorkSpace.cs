﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RoomService.Models;
using RoomService.Models.Utils;
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
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> Features { get; set; }
        public int AllSeats { get; set; }
        public int  Seats { get; set; }
        public string SubMap { get; set; }
        public string Building { get; set; }
        public Point2d Pivot { get; set; }
        [BsonIgnore]
        public string Owner { get => Building; }
    }
}
