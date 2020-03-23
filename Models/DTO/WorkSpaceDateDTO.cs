using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{
    /// <summary>
    /// WorkSpace Id and Date>Time in ISO 8601 format
    /// Used from API requests
    /// </summary>
    public class WorkSpaceDateDTO
    {
        /// <summary>
        /// Id of WorkSpace
        /// </summary>
        public string WorkSpaceId { get; set; }

        /// <summary>
        /// ISO 8601 formatted DateTime
        /// </summary>
        public string Date { get; set; }
    }
}
