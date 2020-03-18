using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using RoomService.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Models

    
{
    /// <summary>
    /// Service for WorSpace
    /// </summary>
    public class WorkSpaceReservations : IModel, IOwnable
    {
        /// <summary>
        /// Id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]

        /// <summary>
        /// Times of DeltaTime
        /// </summary>
        public DeltaTime Times { get; set; }
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]

        ///<summary>
        ///Owner
        /// </summary>
        public string Owner { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]

        /// <summary>
        /// Reservation of IEnumerable
        /// </summary>
        public IEnumerable<string> Reservations { get; set; }
    }
}
