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
        public string UserCollection { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string BuildingCollection { get; set; }
        public string ReservationCollection { get; set; }
        public string WorkSpaceCollection { get; set; }
        public string QRMapCollection { get; set; }
        public string FavouritesCollection { get; set; }
    }
}
