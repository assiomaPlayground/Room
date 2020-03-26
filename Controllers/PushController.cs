﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class PushController : ControllerBase
    {
        private readonly PushService _pushService;
        private readonly string _publicKey;
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

        [HttpPost]
        public ActionResult<PushSubscription> Post([FromBody] PushSubscription subscription)
        {
            return _pushService.Insert(subscription);
        }

        [HttpDelete("{endpoint}")]
        public void Delete([FromRoute] string endpoint)
        {
            _pushService.Delete(WebUtility.UrlDecode(endpoint));
        }
    }
}