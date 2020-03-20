using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Models.Types
{
    /// <summary>
    /// Booking times intervals for buildings and workSpaces
    /// </summary>
    public class DeltaTime
    {
        /// <summary>
        /// StartTime Date in ISO 8601 format
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// EndTime Date in ISO 8601 format
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// Equals override
        /// </summary>
        /// <param name="obj">Other object to evalutate</param>
        /// <returns>Cast the object to self type and return true if all fields matchs</returns>
        public override bool Equals(object obj)
            => ((obj as DeltaTime).StartTime == this.StartTime && (obj as DeltaTime).EndTime == this.EndTime);

        /// <summary>
        /// Get HashCode needed override
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => base.GetHashCode();
    }
}
