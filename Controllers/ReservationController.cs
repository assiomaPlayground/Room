﻿using Microsoft.AspNetCore.Mvc;
using RoomService.DTO;
using RoomService.Models;
using RoomService.Models.Types;
using RoomService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace RoomService.Controllers
{
    /// <summary>
    /// Controller for Reservation collection base Crud in abstract class
    /// </summary>
    public class ReservationController : AbstractMongoCrudController<Reservation, ReservationService>
    {
        private readonly AccessControlService _acs;
        private readonly ReservationUpdaterService _rus;
        public ReservationController(
            ReservationService service,
            AccessControlService acs,
            ReservationUpdaterService rus //Just for instancing
            ) : base(service) 
        {
            this._acs = acs;
            this._rus = rus;
        }
        [HttpGet("User/{id:length(24)}")]
        public ActionResult<IEnumerable<Reservation>> GetUserReservations([FromRoute] string id)
        {
            if (id?.Length < 1)
                return BadRequest();
            if (!this._acs.IsAuth(id))
                return Forbid();
            var res = Service.GetUserReservations(id);
            return new OkObjectResult(res);
        }
        [HttpPost("CheckIn")]
        public IActionResult CheckIn([FromBody] WorkSpaceDateDTO data)
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            var id = Service.FindOnGoindReservationIdByWorkSpaceAndUserIds(data.WorkSpaceId, rid);
            if (id == null)
                return NotFound();
            if (!_acs.IsOwner<ReservationService,Reservation>(rid, id, Service))
                return Forbid();
            if (Service.CheckIn(id, data))
                return Ok();
            return BadRequest();
        }
        [HttpPost("CheckOut")]
        public IActionResult CheckOut([FromBody] WorkSpaceDateDTO data)
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            var id = Service.FindOnGoindReservationIdByWorkSpaceAndUserIds(data.WorkSpaceId, rid);
            if (id == null)
                return NotFound();
            if (!_acs.IsOwner<ReservationService, Reservation>(rid, id, Service))
                return Forbid();
            if (Service.CheckOut(id, data))
                return Ok();
            return BadRequest();
        }
        [HttpGet("WorkSpace/{id:length(24)}/{start}/{end}")]
        public ActionResult<WorkSpaceReservationDTO> GetReservationMeta([FromRoute] string id, [FromRoute] string start, [FromRoute] string end)
        {
            var date = new DeltaTime { StartTime = start, EndTime = end };
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            if (!_acs.IsAuth(rid))
                Forbid();
            return new OkObjectResult(Service.GetReservationByDeltaTimeAdWorkSpaceId(id, date));
        }
        protected override bool CanCreate(string id, Reservation model)
            => _acs.CanCreateReservation(id, model);
        protected override bool CanDelete(string id, string tid)
            => _acs.IsOwner<ReservationService, Reservation>(id, tid, Service);
        protected override bool CanRead(string id, string tid)
            => _acs.OwnOrOnGoing(id, tid);
        protected override bool CanReadAll(string id)
            => _acs.IsAdmin(id);
        protected override bool CanUpdate(string id, Reservation model)
            => _acs.IsOwner<Reservation>(id, model);
    }
}
