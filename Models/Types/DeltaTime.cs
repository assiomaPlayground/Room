using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Models.Types
{

    /// <summary>
    /// booking times for buildings and rooms
    /// </summary>
    public class DeltaTime
    {
        /// <summary>
        /// StartTime 
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// EndTime
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>the result is a new DeltaTime</returns>
        public override bool Equals(object obj)
            => ((obj as DeltaTime).StartTime == this.StartTime && (obj as DeltaTime).EndTime == this.EndTime);

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns>HashCode
        /// </returns>
        public override int GetHashCode()
            => base.GetHashCode();
    }
}
