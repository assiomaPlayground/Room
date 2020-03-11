﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly AccessControlService _acs;
        public FavouritesController(FavouritesService service, AccessControlService acs) : base(service) 
        {
            this._acs = acs;
        }
        protected override bool CanCreate(string id, Favourites model)
            => _acs.IsOwner(id, model);
        protected override bool CanDelete(string id, string tid)
            => _acs.IsOwner<FavouritesService, Favourites>(id, tid, Service);
        protected override bool CanRead(string id, string tid)
            => _acs.IsOwner<FavouritesService, Favourites>(id, tid, Service);
        protected override bool CanReadAll(string id)
            => _acs.IsAdmin(id);
        protected override bool CanUpdate(string id, Favourites model)
            => _acs.IsOwner(id, model);
    }
}
