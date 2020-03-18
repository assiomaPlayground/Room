using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Settings
{
    /// <summary>
    /// Interface for specific mongo database RoomService avable collections
    /// <see cref="RoomServiceMongoSettings"/>
    /// </summary>
    public interface IRoomServiceMongoSettings : IMongoSettings
    {
        /// <summary>
        /// Collection of User
        /// </summary>
        string UserCollection { get; set; } 

        /// <summary>
        /// Collection of Building
        /// </summary>
        string BuildingCollection { get; set; }

        /// <summary>
        /// Collection of Reservation
        /// </summary>
        string ReservationCollection { get; set; }

        /// <summary>
        /// Collection of WorkSpace
        /// </summary>
        string WorkSpaceCollection { get; set; }

        /// <summary>
        /// Collection of QRMap
        /// </summary>
        string QRMapCollection { get; set; }

        /// <summary>
        /// Collection of Favourites
        /// </summary>
        string FavouritesCollection { get; set; }
        string WorkSpaceReservationCollection { get; set; }
    }
}