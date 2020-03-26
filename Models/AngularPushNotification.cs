using Lib.Net.Http.WebPush;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Models
{

    // Push Notification 
    public class AngularPushNotification
    {
        private const string WRAPPER_START = "{\"notification\":";
        private const string WRAPPER_END = "}";

        /// <summary>
        /// Action of Notification
        /// </summary>
        public class NotificationAction
        {

            // String Action
            public string Action { get; }

            // String Title
            public string Title { get; }


            /// <summary>
            /// Action of Notification
            /// </summary>
            /// <param name="action">String action</param>
            /// <param name="title">String title</param>
            public NotificationAction(string action, string title)
            {
                Action = action;
                Title = title;
            }
        }

        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        // String Title
        public string Title { get; set; }

        //String Body
        public string Body { get; set; }

        // String Icon
        public string Icon { get; set; }

        
     
        public IList<int> Vibrate { get; set; } = new List<int>();

        public IDictionary<string, object> Data { get; set; }

        public IList<NotificationAction> Actions { get; set; } = new List<NotificationAction>();

        /// <summary>
        /// Push Message
        /// </summary>
        /// <param name="topic">string topic= null</param>
        /// <param name="timeToLive">int timeToLive= null</param>
        /// <param name="urgency">PushMessageUrgency</param>
        /// <returns>new PushMessage</returns>
        public PushMessage ToPushMessage(string topic = null, int? timeToLive = null, PushMessageUrgency urgency = PushMessageUrgency.Normal)
        {
            return new PushMessage(WRAPPER_START + JsonConvert.SerializeObject(this, _jsonSerializerSettings) + WRAPPER_END)
            {
                Topic = topic,
                TimeToLive = timeToLive,
                Urgency = urgency
            };
        }
    }
}
