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
    /// Service for Favourite collection crud ops in abstract
    /// </summary>
    public class FavouritesService : AbstractMongoCrudService<Favourites>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">Mongo settings file wrapper</param>
        public FavouritesService(IRoomServiceMongoSettings settings)
        {
            base.Init(settings, settings.FavouritesCollection);
        }
    }
}

