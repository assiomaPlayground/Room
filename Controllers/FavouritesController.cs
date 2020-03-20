using RoomService.Models;
using RoomService.Services;

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
        /// <param name="id">string id</param>
        /// <param name="model">favourites model</param>
        /// <returns>Owner(id,model)</returns>
        protected override bool CanCreate(string id, Favourites model)
            => _acs.IsOwner(id, model);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">string id</param>
        /// <param name="tid">string tid</param>
        /// <returns>Owner(id,tid,Service)</returns>
        protected override bool CanDelete(string id, string tid)
            => _acs.IsOwner<FavouritesService, Favourites>(id, tid, Service);

        /// <summary>
        /// Can Read
        /// </summary>
        /// <param name="id">string id</param>
        /// <param name="tid">string tid</param>
        /// <returns>Owner(id,tid,Service)</returns>
        protected override bool CanRead(string id, string tid)
            => _acs.IsOwner<FavouritesService, Favourites>(id, tid, Service);

        /// <summary>
        /// Read All
        /// </summary>
        /// <param name="id">string id</param>
        /// <returns>id Admin</returns>
        protected override bool CanReadAll(string id)
            => _acs.IsAdmin(id);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id">String id</param>
        /// <param name="model">Favourites model</param>
        /// <returns>Is Owner</returns>
        protected override bool CanUpdate(string id, Favourites model)
            => _acs.IsOwner(id, model);
    }
}
