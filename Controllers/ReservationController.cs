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
        protected override bool CanCreate(string id, Reservation model)
            => _acs.IsOwner<Reservation>(id, model);
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
