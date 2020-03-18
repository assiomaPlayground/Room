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

        /// <summary>
        /// reservation status of users
        /// </summary>
        public enum Statuses
        {

            /// <summary>
            /// reservation status of users
            /// Attiva  when the start time is before the current time and whose end time is after the current time
            /// Incorso where the reservation is in force
            /// Conclusa ended when checkout is done
            /// Checkin when the user uses the booked service 
            /// Cancellata when the user cancels his reservation
            /// </summary>
            ATTIVA,
            INCORSO,
            CONCLUSA,
            CHECKIN,
            CANCELLATA
        }


        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]

        /// <summary>
        /// Status
        /// Un valued properties are simply ignored
        /// </summary>
        public Statuses Status { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        /// <summary>
        /// Checkin
        /// Un valued properties are simply ignored
        /// </summary>
        public IEnumerable<string> CheckIn   { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        /// <summary>
        /// Checkout
        /// </summary>
        public IEnumerable<string> CheckOut  { get; set; }
        [BsonRequired]

        /// <summary>
        /// Owner
        /// </summary>
        public string Owner  { get; set; }
        [BsonRequired]

        /// <summary>
        /// Target
        /// Un valued properties are simply ignored
        /// </summary>
        public string Target { get; set; }
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        /// <summary>
        /// Interval of DeltaTime
        /// </summary>
        public DeltaTime Interval { get; set; }
    }
}
