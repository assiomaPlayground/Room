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
        private readonly UserService        _userService;
        private readonly ReservationService _reservationService;
        private readonly WorkSpaceService   _workSpaceService;
        public AccessControlService(
            UserService userService,
            ReservationService reservationService,
            WorkSpaceService workSpaceService
        )
        {
            this._userService = userService;
            this._reservationService = reservationService;
            this._workSpaceService = workSpaceService;
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
            =>  ( 
                (IsAdmin(id) ? true : (model.UserType == UserModel.UserTypes.USER)) &&
                _userService.FindByUserName(model.Username) == null //Unique username
            );
        //Reservation
        public bool OnGoind(Reservation reservation)
            => reservation.Status == (int)Reservation.Statuses.ATTIVA;
        public bool OwnOrOnGoing(string id, string tid) 
        {
            if (IsAdmin(id))
                return true;
            var res = _reservationService.Read(tid);
            return OnGoind(res) || IsOwner<Reservation>(id, res);
        }
        public bool CanCreateReservation(string id, Reservation model)
        {
            if (!model.IsValid()) //Invalid
                return false;
            if (!IsOwner<Reservation>(id, model)) //Forbid
                return false;

            /* var count = _reservationService.Collection.Find
            (
                x => x.Target == model.Target &&
                (
                    x.Status == Reservation.Statuses.ATTIVA ||
                    x.Status == Reservation.Statuses.CHECKIN ||
                    x.Status == Reservation.Statuses.INCORSO
                )
           ).CountDocuments();

            if (count < 1)
                return false; 

            HashSet<Reservation.Statuses> filter = new HashSet<Reservation.Statuses>
            { Reservation.Statuses.ATTIVA, Reservation.Statuses.CHECKIN, Reservation.Statuses.INCORSO };

            var qres = from res in _reservationService.Collection.AsQueryable()
                       where id == res.Owner && filter.Contains(res.Status)
                       select res;

            foreach (var userRes in qres)
                if (DataInsersects(userRes.StartTime, userRes.ExitTime, model.StartTime, model.ExitTime))
                    return false;
            */
            return true;
        }

        private bool DataInsersects(string start, string end, string secStart, string secEnd)
        => !(string.Compare(start, secStart) < 0 ?
            string.Compare(end, secStart) < 0 : string.Compare(secEnd, start) < 0);
    }
}
