using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomService.Models;
using RoomService.Services;

namespace RoomService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // Push Controller
    // Controller Base
    public class PushController : ControllerBase
    {
        private readonly PushService _pushService;
        private readonly string _publicKey;

        /// <summary>
        /// push Controller
        /// </summary>
        /// <param name="pushService">push Service(publicKey)</param>
        public PushController(PushService pushService)
        {
            this._pushService = pushService;
            this._publicKey = "BPWObu3Sq-QOaJiOS0CXNEKP9_r2Sm4qWnSXi6k4cDiyb6C-BhviCi9m7VK9jWJcYb75CfPDSrRbcg-M3a4wOV0";
        }

        [HttpGet("SubKey")]
        public ContentResult GetKey()
        {
            return Content(_publicKey, "text/plain");
        }


        /// <summary>
        /// Action Result Push Subcription
        /// </summary>
        /// <param name="subscription">Action Result Post</param>
        /// <returns>Insert subscription</returns>
        [HttpPost]
        public ActionResult<PushSubscription> Post([FromBody] PushSubscription subscription)
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            var wrp = new SubscriptionWrapper { Subscription = subscription };
            if (rid != null)
                wrp.Owner = rid;
            return _pushService.Insert(wrp);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="endpoint">Delete push Service</param>
        [HttpDelete("{endpoint}")]
        public void Delete([FromRoute] string endpoint)
        {
            _pushService.Delete(WebUtility.UrlDecode(endpoint));
        }
    }
}