using Newtonsoft.Json;
using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{
    /// <summary>
    /// Possibility to choose between various spaces and buildings
    /// </summary>
    public class BuildingWorkSpaceDTO
    {
        /// <summary>
        /// The Building Data
        /// </summary>
        public Building Building { get; set; }
        /// <summary>
        /// The WorkSpaces in the building
        /// </summary>
        public IEnumerable<WorkSpace> WorkSpaces { get; set; }
    }
}
