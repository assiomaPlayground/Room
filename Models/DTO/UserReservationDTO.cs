using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{
    public class UserReservationDTO
    {
        public UserModel User { get; set; }
        public Reservation Reservation { get; set; }
    }
}
