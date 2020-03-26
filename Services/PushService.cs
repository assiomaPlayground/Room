using MongoDB.Driver;
using RoomService.Models;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;

namespace RoomService.Services
{

    // Push Service
    public class PushService
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<SubscriptionWrapper> _collection;
        private readonly PushServiceClient _pushClient;

        /// <summary>
        /// Push Service
        /// </summary>
        /// <param name="settings">Mongo Client</param>
        public PushService(IRoomServiceMongoSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            this._database = client.GetDatabase(settings.DatabaseName);

            _collection = _database.GetCollection<SubscriptionWrapper>("Subscriptions");

            this._pushClient = new PushServiceClient
            {
                DefaultAuthentication = new VapidAuthentication
                ("BPWObu3Sq-QOaJiOS0CXNEKP9_r2Sm4qWnSXi6k4cDiyb6C-BhviCi9m7VK9jWJcYb75CfPDSrRbcg-M3a4wOV0", 
                "tUOAgJJTZhH5bNafx-t0CjBx-3K-H3TAMKK2PuhHCK8")
            };
        }

        /// <summary>
        /// Push Subrciption
        /// </summary>
        /// <param name="subscription">Subscrition wrapper</param>
        /// <returns>subscription</returns>
        public PushSubscription Insert(PushSubscription subscription)
        {
            _collection.InsertOne(new SubscriptionWrapper { Subscription = subscription });
            return subscription;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="endpoint">Subscription Endpoint</param>
        public void Delete(string endpoint)
        {
            _collection.DeleteOne(wrapper => wrapper.Subscription.Endpoint == endpoint);
        }

        /// <summary>
        /// IEnumerable Get Subscribers
        /// </summary>
        /// <returns>Find if x=true; Select if x=x.Subscription</returns>
        public IEnumerable<PushSubscription> GetSubscribers()
        {
            return this._collection.Find(x => true).ToEnumerable().Select(x => x.Subscription);
        }

        /// <summary>
        /// Send Notification
        /// </summary>
        /// <param name="message">string message</param>
        public void SendNotifications(string message)
        {
            
            // Notification Push Message
            PushMessage notification = new AngularPushNotification
            {

                Title = "Aggiornamento nella reservation", //Title
                Body = message, //Body
                Icon = "../ClientApp/src/assets/icons/icon-96x96.png" //Icon
            }.ToPushMessage();

            
            foreach (PushSubscription subscription in GetSubscribers())
            {
                _pushClient.RequestPushMessageDeliveryAsync(subscription, notification);
            }
        }
    }
}