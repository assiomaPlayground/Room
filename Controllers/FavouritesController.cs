using Microsoft.AspNetCore.Mvc;
using RoomService.Models;
using RoomService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Controllers
{
    /// <summary>
    /// Controller for favourites collection base Crud in abstract class
    /// </summary>
    public class FavouritesController : AbstractMongoCrudController<Favourites, FavouritesService>
    {
        public FavouritesController(FavouritesService service) : base(service) { }
    }
}
