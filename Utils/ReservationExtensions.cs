using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomService.Models;

namespace RoomService.Utils
{
    public static partial class ExtensionMethods
    {
        public static bool IsValid(this Reservation reservation)
            => (string.Compare(reservation.StartTime, reservation.ExitTime) < 0);
    }
}
