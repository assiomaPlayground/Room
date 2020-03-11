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
    /// Model for reservation data
    /// </summary>
    public class Reservation : IModel, IOwnable
    {
        public enum Status
        {
            ATTIVA,
            INCORSO,
            CONCLUSA,
            CHECKIN,
            CANCELLATA
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Status Stato { get; set; }
        public List<DateTime> Ingresso { get; set; }
        public List<DateTime> Uscita { get; set; }
        public string Owner  { get; set; }
        public string Target { get; set; }
    }
}
