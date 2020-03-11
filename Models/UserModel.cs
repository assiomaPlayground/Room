using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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
        public string Password { get; set; }
        [BsonIgnore]
        public string Token { get; set; }
        public string Photo { get; set; }
        public UserTypes UserType { get; set; }
        [BsonIgnore]
        public string Owner {get => Id; }
    }
}
