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
    /// Model for reservation data
    /// </summary>
    public class Reservation : IModel, IOwnable
    {
        public enum Statuses
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
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public Statuses Status { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> CheckIn   { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> CheckOut  { get; set; }
        [BsonRequired]
        public string Owner  { get; set; }
        [BsonRequired]
        public string Target { get; set; }
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DeltaTime Day { get; set; }
    }
}
