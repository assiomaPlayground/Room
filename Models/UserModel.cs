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
            /// User who is given the opportunity to reserve a seat in the WorkSpace to which access is provided
            /// </summary>
            USER,
            /// <summary>
            /// Admin which configures, manages and maintains the reservation of jobs and meeting WorkSpaces
            /// </summary>
            ADMIN
        }
        /// <summary>
        /// Id <see cref="IModel.Id"/>
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// Username this field is also a key in the database
        /// </summary>
        [BsonRequired]
        public string Username { get; set; }

        /// <summary>
        /// Password this field is stored encrypted in db and removed in responses
        /// Un valued properties are simply ignored
        /// </summary>
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        /// <summary>
        /// Token this field is excluded in database is used for user identification on API calls
        /// Un valued properties are simply ignored
        /// </summary>
        [BsonIgnore]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        /// <summary>
        /// Photo url to user picture/avatar
        /// Un valued properties are simply ignored
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Photo { get; set; }

        /// <summary>
        /// User identification type
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public UserTypes UserType { get; set; }

        /// <summary>
        /// Owner of the resource user are self owned // own their profile
        /// <see cref="IOwnable"/>
        /// </summary>
        [BsonIgnore][JsonIgnore]
        public string Owner {get => Id; }
    }
}
