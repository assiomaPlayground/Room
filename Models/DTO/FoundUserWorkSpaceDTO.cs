using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{

    /// <summary>
    /// Work Space User
    /// </summary>
    public class FoundUserWorkSpaceDTO
    {

        /// <summary>
        /// User 
        /// </summary>
     
        public UserModel User { get; set; }

        /// <summary>
        /// Reservation of WorkSpace
        /// </summary>
        public WorkSpaceReservationDTO WorkSpaceReservation { get; set; }
    }
}
