using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Settings
{
    /// <summary>
    /// Settings for RoomService mongo connection app
    /// </summary>
    public class RoomServiceMongoSettings : IRoomServiceMongoSettings
    {
        /// <summary>
        /// Connection string for UserCollection
        /// </summary>
        public string UserCollection { get; set; }

        /// <summary>
        /// Connection string for mongoclient
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Connection string for database
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Connection string for BuildingCollection
        /// </summary>
        public string BuildingCollection { get; set; }

        /// <summary>
        /// Connection string for ReservationCollection
        /// </summary>
        public string ReservationCollection { get; set; }

        /// <summary>
        /// Connection string for WorkSpaceCollection
        /// </summary>
        public string WorkSpaceCollection { get; set; }

        /// <summary>
        /// /// Connection string for QRMapCollection
        /// </summary>
        public string QRMapCollection { get; set; }

        /// <summary>
        /// Connection string for FavouritesCollection
        /// </summary>
        public string FavouritesCollection { get; set; }

        /// <summary>
        /// Connection string for WorkSpaceReservationCollection
        /// </summary>
        public string WorkSpaceReservationCollection { get; set; }
    }
}
