using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Models
{
    /// <summary>
    /// Interface for Ownable data
    /// </summary>
    public interface IOwnable
    {
        /// <summary>
        /// String Owner
        /// </summary>
        public string Owner { get; }
    }
}
