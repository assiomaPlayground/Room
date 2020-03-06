using Microsoft.AspNetCore.Mvc;
using RoomService.Model;
using RoomService.Models;
using RoomService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RoomService.Controllers
{
    public class ReservationController : AbstractMongoCrudController<Reservation, ReservationService>
    {
        public ReservationController(ReservationService service) : base(service) { }
    }
}
