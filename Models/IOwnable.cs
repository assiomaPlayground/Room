using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Models
{

    /// <summary>
    /// Interface for Ownable
    /// <see cref="Ownable"/>
    /// </summary>
    public interface IOwnable
    {

        /// <summary>
        /// Owner
        /// </summary>
        public string Owner { get; }
    }
}
