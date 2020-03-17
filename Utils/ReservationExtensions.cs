using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Utils
{
    public static class ReservationExtensions
    {
        public static Reservation Clone(this Reservation target)
            => new Reservation
            {
                Id = target.Id,
                CheckIn = target.CheckIn,
                CheckOut = target.CheckOut,
                Interval = target.Interval,
                Owner = target.Owner,
                Status = target.Status,
                Target = target.Target
            };
    }
}
