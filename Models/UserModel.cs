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
        public enum UserTypes
        {
            USER,
            ADMIN
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public string Username { get; set; }
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }
        [BsonIgnore]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Photo { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public UserTypes UserType { get; set; }
        [BsonIgnore][JsonIgnore]
        public string Owner {get => Id; }
    }
}
