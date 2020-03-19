using RoomService.Models;
using RoomService.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
    
{
    /// <summary>
    /// Avaialability of WorkSpace
    /// </summary>
    public class WorkSpaceAvailabilityDTO
    {
        /// <summary>
        /// Id of Reservation
        /// </summary>
        public string ReservationId { get; set; }

        /// <summary>
        /// Target of WorkSpace
        /// </summary>
        public WorkSpace TargetWorkSpace { get; set; }

        /// <summary>
        /// Interval of DeltaTime
        /// </summary>
        public DeltaTime Interval { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Availability { get; set; }
    }
}
