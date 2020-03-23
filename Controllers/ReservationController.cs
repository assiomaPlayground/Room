using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Controller of Reservation
        /// </summary>
        /// <param name="service">ReservationService</param>
        /// <param name="acs">AccessControlService</param>
        /// <param name="rus">ReservationUpdaterService</param>
        public ReservationController(
            ReservationService service,
            AccessControlService acs,
            ReservationUpdaterService rus //Just for instancing
            ) : base(service) 
        {
            this._acs = acs;
            this._rus = rus;
        }

        /// <summary>
        /// Reservation User
        /// </summary>
        /// <param name="id">The user id : 24 string</param>
        /// <returns>Bad Request, Forbid, new Object Result</returns>

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

        /// <summary>
        /// Checkin
        /// </summary>
        /// <param name="data">WorkspaceDateDTO</param>
        /// <returns>Not Found,Forbid,Bad Request</returns>
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
        /// <summary>
        /// Checkout
        /// </summary>
        /// <param name="data">WorkSpaceDate</param>
        /// <returns> Not Found, Forbid, Bad Request</returns>
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

        /// <summary>
        /// ReservationMeta
        /// </summary>
        /// <param name="id">The user id : 24 string</param>
        /// <returns>Forbid, new Object Result></returns>
        [HttpGet("WorkSpace/{id:length(24)}")]
        public ActionResult<WorkSpaceReservationDTO> GetReservationMeta([FromRoute] string id)
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            if (!_acs.IsAuth(rid))
                Forbid();
            return new OkObjectResult(Service.GetReservationMeta(id));
        }

        /// <summary>
        /// Reservation and WorkSpaces User(String id)
        /// </summary>
        /// <param name="id">The user id : 24 string</param>
        /// <returns>Forbid, new Object Result</returns>
        /// 
        [HttpGet("WorkSpace/User/{id:length(24)}")]
        public ActionResult<IEnumerable<WorkSpaceReservationDTO>> GetUserReservationsAndWorkSpaces(string id)
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            if (!this._acs.IsAuth(rid))
                return Forbid();
            return new OkObjectResult(Service.GetUserReservationsAndWorkSpaces(id));
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model">Reservation model</param>
        /// <returns>Forbid,Bad Request,new Object Result</returns>
        public override ActionResult<Reservation> Create([FromBody] Reservation model)
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            if (!CanCreate(rid, model))
                return Forbid();
            model = Service.Create(model);
            if (model.Id == null)
                return BadRequest();
            return new OkObjectResult(model);
        }

        /// <summary>
        /// CanCreate
        /// </summary>
        /// <param name="id">string id</param>
        /// <param name="model">Reservation model</param>
        /// <returns>CreateReservation(id,model)</returns>
        protected override bool CanCreate(string id, Reservation model)
            => _acs.CanCreateReservation(id, model);
        /// <summary>
        /// Can Delete
        /// </summary>
        /// <param name="id">string id</param>
        /// <param name="tid">string tid</param>
        /// <returns>Reservation Service(id,tid,Service)</returns>
        protected override bool CanDelete(string id, string tid)
            => _acs.IsOwner<ReservationService, Reservation>(id, tid, Service);

        /// <summary>
        /// Can Read
        /// </summary>
        /// <param name="id">string id</param>
        /// <param name="tid">string tid</param>
        /// <returns>Id Auth</returns>
        protected override bool CanRead(string id, string tid)
            => _acs.IsAuth(id);

        /// <summary>
        /// Can Read All
        /// </summary>
        /// <param name="id">string id</param>
        /// <returns>id Admin</returns>
        protected override bool CanReadAll(string id)
            => _acs.IsAdmin(id);

        /// <summary>
        /// Can Update
        /// </summary>
        /// <param name="id">string id</param>
        /// <param name="model">Reservation model</param>
        /// <returns>Owner(id,model)</returns>
        protected override bool CanUpdate(string id, Reservation model)
            => _acs.IsOwner<Reservation>(id, model);
    }
}
