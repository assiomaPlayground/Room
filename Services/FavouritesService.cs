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
        private readonly WorkSpaceService _workSpaceService;
        private readonly UserService      _userService;
        public FavouritesService(
            IRoomServiceMongoSettings settings, 
            WorkSpaceService workSpaceService,
            UserService userService
        )
        {
            this._workSpaceService = workSpaceService;
            this._userService = userService;
            base.Init(settings, settings.FavouritesCollection);
        }

        public DeleteResult DeleteByUserId(string id)
            => Collection.DeleteMany(fav => fav.Owner == id);

        public UserFavouriteRoomsDTO GetUserFavouritesRooms(string id)
        {
            var favs = Collection.Find(fav => fav.Owner == id).ToEnumerable();
            var user = _userService.Read(id);
            var qres = from fav in favs.AsQueryable()
                       join room in _workSpaceService.Collection.AsQueryable() on fav.Target equals room.Id
                       select new UserFavouriteRoomsDTO.FavouriteRoom
                       {
                           Workspace = room,
                           Last = fav.Last,
                           UsageTimes = fav.UsageTimes
                       };
            return new UserFavouriteRoomsDTO { Owner = user, Rooms = qres.ToArray() };
        }
    }
}

