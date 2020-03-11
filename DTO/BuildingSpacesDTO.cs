using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{
    public class BuildingSpacesDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Map { get; set; }
        public IEnumerable<WorkSpace> Rooms { get; set; }
    }
}
