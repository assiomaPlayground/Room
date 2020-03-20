using Newtonsoft.Json;
using RoomService.Models;
using RoomService.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{
    /// <summary>
    /// WorkSpace Reservation DTO has id instead of string and less metadata
    /// <see cref="WorkSpaceReservation"/>
    /// </summary>
    public class WorkSpaceReservationDTO
    {
        /// <summary>
        /// The workspace of the reservation
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public WorkSpace WorkSpace { get; set; }
        /// <summary>
        /// WorkSpace Reservation Interval time
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public DeltaTime Interval { get; set; }
        /// <summary>
        /// Number of users with a reservation in the Workspace during Interval time
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public int Users { get; set; }
    }
}
