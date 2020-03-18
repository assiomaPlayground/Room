using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Models
{
    /// <summary>
    /// Model for user data
    /// </summary>
    public class UserModel : IModel, IOwnable
    {

        /// <summary>
        /// UserTypes
        /// </summary>
        public enum UserTypes
        {

            /// <summary>
            /// User who is given the opportunity to reserve a seat in the rooms to which access is provided
            /// Admin which configures, manages and maintains the reservation of jobs and meeting rooms
            /// </summary>
            USER,
            ADMIN
        }

        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        [BsonRequired]

        /// <summary>
        /// Username
        /// Un valued properties are simply ignored
        /// </summary>
        public string Username { get; set; }
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        /// <summary>
        /// Password
        /// Un valued properties are simply ignored
        /// </summary>
        public string Password { get; set; }
        [BsonIgnore]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        /// <summary>
        /// Token
        /// Un valued properties are simply ignored
        /// </summary>
        public string Token { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]


        /// <summary>
        /// Photo
        /// </summary>
        public string Photo { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]

        /// <summary>
        /// UserType
        /// </summary>
        public UserTypes UserType { get; set; }
        [BsonIgnore][JsonIgnore]

        /// <summary>
        /// Owner
        /// </summary>
        public string Owner {get => Id; }
    }
}
