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

        //Id
        public string Id { get; set; }
        [BsonRequired]

        //Username
        public string Username { get; set; }
        [BsonRequired]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        //Password
        public string Password { get; set; }
        [BsonIgnore]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        //Token
        public string Token { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        //Photo
        public string Photo { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]

        //UserType
        public UserTypes UserType { get; set; }
        [BsonIgnore][JsonIgnore]

        //Owner
        public string Owner {get => Id; }
    }
}
