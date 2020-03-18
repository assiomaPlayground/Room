using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Models
{
    /// <summary>
    /// Model for compaibility interface in generic use
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
    }
}
