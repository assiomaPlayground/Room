using Microsoft.AspNetCore.Mvc;
using RoomService.Models;
using RoomService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RoomService.Controllers
{
    /// <summary>
    /// Controller for Reservation collection base Crud in abstract class
    /// </summary>
    public class ReservationController : AbstractMongoCrudController<Reservation, ReservationService>
    {
        private readonly AccessControlService _acs;
        public ReservationController(ReservationService service, AccessControlService acs) : base(service) 
        {
            this._acs = acs;
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
