using Microsoft.AspNetCore.Mvc;
using RoomService.DTO;
using RoomService.Models;
using RoomService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoomService.Controllers
{
    /// <summary>
    /// Controller for favourites collection base Crud in abstract class
    /// </summary>
    public class FavouritesController : AbstractMongoCrudController<Favourites, FavouritesService>
    {

        /// <summary>
        /// acs as AccessControlService
        /// </summary>
        private readonly AccessControlService _acs;

        /// <summary>
        /// Controller for favourites collection
        /// </summary>
        /// <param name="service">Favourite service</param>
        /// <param name="acs">Access Control Service</param>
        public FavouritesController(FavouritesService service, AccessControlService acs) : base(service) 
        {
            this._acs = acs;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override bool CanCreate(string id, Favourites model)
            => _acs.IsOwner(id, model);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        protected override bool CanDelete(string id, string tid)
            => _acs.IsOwner<FavouritesService, Favourites>(id, tid, Service);

        /// <summary>
        /// Can Read
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        protected override bool CanRead(string id, string tid)
            => _acs.IsOwner<FavouritesService, Favourites>(id, tid, Service);

        /// <summary>
        /// Read All
        /// </summary>
        /// <param name="id"></param>
        /// <returns>id Admin</returns>
        protected override bool CanReadAll(string id)
            => _acs.IsAdmin(id);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override bool CanUpdate(string id, Favourites model)
            => _acs.IsOwner(id, model);
    }
}
