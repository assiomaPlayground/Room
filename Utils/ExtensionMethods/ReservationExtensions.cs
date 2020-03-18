using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Utils
{
    /// <summary>
    /// Extension for Reservation class
    /// </summary>
    public static class ReservationExtensions
    {
        /// <summary>
        /// Make a full clone of the target class
        /// </summary>
        /// <param name="target">The target to copy</param>
        /// <returns>A new copy of Reservation targhet</returns>
        public static Reservation Clone(this Reservation target)
            => new Reservation
            {
                Id = target.Id,
                Interval = new Models.Types.DeltaTime { StartTime = target.Interval.StartTime, EndTime = target.Interval.EndTime },
                Owner = target.Owner,
                Status = target.Status,
                Target = target.Target
            };
    }
}
