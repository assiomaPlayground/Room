using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{
    public class WorkSpaceAvailabilityDTO
    {
        public WorkSpace TargetWorkSpace { get; set; }
        public int Availability { get; set; }
    }
}
