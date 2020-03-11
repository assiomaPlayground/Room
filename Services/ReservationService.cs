using RoomService.Models;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Services
{
    /// <summary>
    /// Service for Reservation collection crud ops in abstract
    /// </summary>
    public class ReservationService : AbstractMongoCrudService<Reservation>
    {
        public ReservationService(IRoomServiceMongoSettings settings)
            => base.Init(settings, settings.ReservationCollection);
    }
}
