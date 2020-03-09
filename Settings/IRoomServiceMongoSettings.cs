using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Settings
{
    public interface IRoomServiceMongoSettings : IMongoSettings
    {
        string UserCollection { get; set; }
        string BuildingCollection { get; set; }
        string ReservationCollection { get; set; }
        string WorkSpaceCollection { get; set; }
        string QRMapCollection { get; set; }
        string FavouritesCollection { get; set; }
    }
}