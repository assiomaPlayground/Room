using RoomService.Models;
using RoomService.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{
    public class WorkSpaceAvailabilityDTO
    {
        public string ReservationId { get; set; }
        public WorkSpace TargetWorkSpace { get; set; }
        public DeltaTime Interval { get; set; }
        public int Availability { get; set; }
    }
}
