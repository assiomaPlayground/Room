using MongoDB.Driver;
using RoomService.DTO;
using RoomService.Models;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Services
{
    /// <summary>
    /// Service for Building collection crud ops in abstract
    /// </summary>
    public class FavouritesService : AbstractMongoCrudService<Favourites>
    {
        public FavouritesService(
            IRoomServiceMongoSettings settings
        )
        {
            base.Init(settings, settings.FavouritesCollection);
        }

        public DeleteResult DeleteByUserId(string id)
            => Collection.DeleteMany(fav => fav.Owner == id);
    }
}

