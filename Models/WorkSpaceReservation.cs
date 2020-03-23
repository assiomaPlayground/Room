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
    public class WorkSpaceReservation : IModel, IOwnable
    {
        /// <summary>
        /// Id <see cref="IModel.Id"/>
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// The ref interval of time the resource refers
        /// </summary>
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public DeltaTime Interval { get; set; }

        ///<summary>
        ///Owner of the resource in this case the WorkSpace
        ///<see cref="IOwnable"/>
        ///<seealso cref="WorkSpace"/>
        /// </summary>
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string Owner { get; set; }

        /// <summary>
        /// Counter of current reservations
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public int Reservations { get; set; }
    }
}
