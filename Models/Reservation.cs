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
        /// Reservation status of users
        /// <para>
        ///     Attiva  when the start time is before the current time and whose end time is after the current time
        ///     Incorso where the reservation is in force
        ///     Conclusa ended when checkout is done
        ///     Checkin when the user uses the booked service 
        ///     Cancellata when the user cancels his reservation
        /// </para>
        /// </summary>
        public enum Statuses
        {
            ATTIVA,
            INCORSO,
            CONCLUSA,
            CHECKIN,
            CANCELLATA
        }
        /// <summary>
        /// Id <see cref="IModel"/>
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// Status
        /// Required reservation status prop
        /// </summary>
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public Statuses Status { get; set; }
        /// <summary>
        /// Checkin
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> CheckIn   { get; set; }
        /// <summary>
        /// Checkout
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> CheckOut  { get; set; }
        /// <summary>
        /// Owner in this case the user of the reservation
        /// Refers the UserId
        /// <see cref="IOwnable"/>
        /// </summary>
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string Owner  { get; set; }
        /// <summary>
        /// Target refers the WorkSpace Id
        /// Un valued properties are simply ignored
        /// @TODO: ITargetable?
        /// </summary>
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string Target { get; set; }
        /// <summary>
        /// Interval of DeltaTime describes the reservation start and end day/hour
        /// </summary>
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public DeltaTime Interval { get; set; }
        /// <summary>
        /// Refers WorkSpaceReservations Id
        /// the reservation instance in workspace and deltatime context
        /// <see cref="WorkSpaceReservations"/>
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string ReservationSocket { get; set; }
    }
}
