using RoomService.Model;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Services
{
    public class ReservationService : AbstractMongoCrudService<Reservation>
    {
        public ReservationService(IRoomServiceMongoSettings settings)
            => base.Init(settings, settings.ReservationCollection);
    }
}
