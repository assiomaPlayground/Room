using RoomService.Models;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Services
{
    public class FavouritesService : AbstractMongoCrudService<Favourites>
    {
       public FavouritesService(IRoomServiceMongoSettings settings)
            => base.Init(settings, "Favourites");
    }
}

