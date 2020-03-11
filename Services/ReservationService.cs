using MongoDB.Driver;
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

        public DeleteResult DeleteByUserId(string id)
            => Collection.DeleteMany(res => res.Owner == id);

        public DeleteResult DeleteByRoomId(string id)
            => Collection.DeleteMany(res => res.Target == id);

        public IEnumerable<Reservation> GetUserReservations(string id)
            => Collection.Find(res => res.Owner == id).ToEnumerable();
    }
}
