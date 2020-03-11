using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Services
{
    /// <summary>
    /// Implementa la logica di sicurezza per le crud
    /// @TODO alcune query tipo IsAdmin donvebbero essere salvate come cache nei Claim per ottimizzare le performance
    /// </summary>
    public class AccessControlService
    {
        private readonly UserService        _userService;
        private readonly ReservationService _reservationService;
        public AccessControlService(UserService userService, ReservationService reservation)
        {
            this._userService = userService;
            this._reservationService = reservation;
        }
        //Shareds
        public bool IsAdmin(string id)
            => ((id != null) && (_userService.Read(id).UserType == UserModel.UserTypes.ADMIN));
        public bool IsAuth(string id)
            => id != null;
        public bool IsOwner<TService, TModel>(string id, string tid, in TService _service)
            where TService : AbstractMongoCrudService<TModel> 
            where TModel : class, IModel, IOwnable
                => IsAdmin(id) ? true : _service.Read(tid).Owner == id;
        public bool IsOwner<TModel>(string id, TModel model)
            where TModel : class, IModel, IOwnable
                => IsAdmin(id) ? true : model.Owner == id;
        //User
        public bool CanCreateUser(string id, UserModel model)        
            => IsAdmin(id) ? true : (model.UserType == UserModel.UserTypes.ADMIN);
        //Reservation
        public bool OnGoind(Reservation reservation)
            => reservation.Stato == Reservation.Status.ATTIVA;
        public bool OwnOrOnGoing(string id, string tid) 
        {
            if (IsAdmin(id))
                return true;
            var res = _reservationService.Read(tid);
            return OnGoind(res) || IsOwner<Reservation>(id, res);
        }
    }
}
