using Lib.Net.Http.WebPush;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Models
{
    public class SubscriptionWrapper : IModel, IOwnable
    {
        /// <summary>
        /// Id <see cref="IModel.Id"/>
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Owner { get; set; }

        [BsonRequired][JsonRequired]
        public PushSubscription Subscription { get; set; }

    }
}
