using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomService.Utils;
using MongoDB.Driver;

namespace RoomService.Services
{
    /// <summary>
    /// Implementa la logica di sicurezza per le crud
    /// @TODO alcune query tipo IsAdmin donvebbero essere salvate come cache nei Claim per ottimizzare le performance
    /// @TODO fare access control service divisi per responsabilitá
    /// </summary>
    public class AccessControlService
    {
        /// <summary>
        /// The User service dependency
        /// </summary>
        private readonly UserService        _userService;

        /// <summary>
        /// The reservation service dependency
        /// </summary>
        private readonly ReservationService _reservationService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userService">Injected user service</param>
        /// <param name="reservationService">Injected reservation service</param>
        public AccessControlService( UserService userService, ReservationService reservationService)
        {
            this._userService = userService;
            this._reservationService = reservationService;
        }

        /// <summary>
        /// Is Admin check
        /// </summary>
        /// <param name="id">User to check</param>
        /// <returns>Bool true : Is Adim</returns>
        public bool IsAdmin(string id)
            => ((id != null) && (_userService.Read(id).UserType == UserModel.UserTypes.ADMIN));

        /// <summary>
        /// Is Auth check
        /// </summary>
        /// <param name="id">User to check</param>
        /// <returns>Bool true : Is Authed</returns>
        public bool IsAuth(string id)
            => id != null;

        /// <summary>
        /// Determinate is an user is Owner of a resource
        /// </summary>
        /// <typeparam name="TService">The service type that will handle the check</typeparam>
        /// <typeparam name="TModel">The model type</typeparam>
        /// <param name="id">The user id</param>
        /// <param name="tid">The targeted resource id</param>
        /// <param name="_service">The service that has access to the resource</param>
        /// <returns>Bool true : Is Owner</returns>
        public bool IsOwner<TService, TModel>(string id, string tid, in TService _service)
            where TService : AbstractMongoCrudService<TModel> 
            where TModel : class, IModel, IOwnable
                => IsAdmin(id) ? true : _service.Read(tid).Owner == id;

        /// <summary>
        /// Is Owner Overload
        /// <seealso cref="IsOwner{TService, TModel}(string, string, in TService)"/>
        /// </summary>
        /// <typeparam name="TModel">The resource type</typeparam>
        /// <param name="id">the user id</param>
        /// <param name="model">the entire resouce</param>
        /// <returns>Bool : true Is Owner</returns>
        public bool IsOwner<TModel>(string id, TModel model)
            where TModel : class, IModel, IOwnable
                => IsAdmin(id) ? true : model.Owner == id;

        /// <summary>
        /// User creation check
        /// Need to be admin or unregistered
        /// And only admin can create admins
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="model">User Model</param>
        /// <returns>True : user can be created</returns>
        public bool CanCreateUser(string id, UserModel model)        
            =>  ( 
                (IsAdmin(id) ? true : (model.UserType == UserModel.UserTypes.USER)) &&
                _userService.FindByUserName(model.Username) == null //Unique username
            );

        /// <summary>
        /// Can create reservation check
        /// Rely on <see cref="ReservationService.CanCreateReservation(Reservation)"/>
        /// </summary>
        /// <param name="id">Creator user Id</param>
        /// <param name="model">The reservation model</param>
        /// <returns>True : can create</returns>
        public bool CanCreateReservation(string id, Reservation model)
        {
            //Forbid
            if (!IsOwner<Reservation>(id, model)) 
                return false;
            return _reservationService.CanCreateReservation(model);
        }
    }
}
