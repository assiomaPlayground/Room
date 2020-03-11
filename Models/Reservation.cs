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
        public enum DayTimes
        {
            MATTINA,
            POMERIGGIO,
            SERA
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonIgnore]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Statuses Status { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> CheckIn   { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> CheckOut  { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string StartTime { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string ExitTime  { get; set; }
        [BsonRequired]
        public string Owner  { get; set; }
        [BsonRequired]
        public string Target { get; set; }
        [BsonIgnore]
        [JsonIgnore]
        public DayTimes DayTime { get; set; }
    }
}
