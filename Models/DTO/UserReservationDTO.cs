using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{
    
    // Reservation User
    public class UserReservationDTO
    {
        // User
        public UserModel User { get; set; }

        // Reservation
        public Reservation Reservation { get; set; }
    }
}
