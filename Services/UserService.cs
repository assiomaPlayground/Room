using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using RoomService.DTO;
using RoomService.Models;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using RoomService.Utils;

namespace RoomService.Services
{
    /// <summary>
    /// Service for User collection crud ops in abstract
    /// @TODO: refactor -- less responsabilities -- new classes
    /// </summary>
    public class UserService : AbstractMongoCrudService<UserModel>
    {
        private readonly string _sectet;
        private readonly double _TokenLifetime;
        private readonly CrypProvider _cryptProvider;

        private readonly ReservationService _reservationService;
        private readonly FavouritesService  _favouriteService;
        private readonly WorkSpaceService   _workSpaceService;
        public UserService(
            IRoomServiceMongoSettings settings, 
            IAppSettings appSettings, 
            CrypProvider cryptProvider,
            ReservationService reservationService,
            FavouritesService favouritesService,
            WorkSpaceService workSpaceService
        )
        {
            base.Init(settings, settings.UserCollection);
            this._sectet = appSettings.Secret;
            this._cryptProvider = cryptProvider;
            this._TokenLifetime = appSettings.TokenDuration;

            this._reservationService = reservationService;
            this._favouriteService = favouritesService;
            this._workSpaceService = workSpaceService;
        }

        public UserModel Register(UserModel model)
        {
            this.Create(model);
            return this.Login(new AuthDTO { Password = model.Password, Username = model.Username });
        }

        public override void Create(UserModel model)
        {
            model.Password = _cryptProvider.Encrypt(model.Password);
            base.Create(model);
        }

        public override ReplaceOneResult Update(string id, UserModel newModel)
        {
            newModel.Password = _cryptProvider.Encrypt(newModel.Password);
            return base.Update(id, newModel);
        }

        public UserModel Login(AuthDTO authData)
        {
            authData.Password = _cryptProvider.Encrypt(authData.Password);
            var user = Collection.Find<UserModel>
                (user =>
                     user.Username == authData.Username 
                ).FirstOrDefault<UserModel>();
            if (user == null) return null;
            if (user.Password != authData.Password)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._sectet);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(_TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            
            return user;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override DeleteResult Delete(string id)
        {
            _reservationService.DeleteByUserId(id); //Delete user reservation //Cascade delete
            _favouriteService.DeleteByUserId(id); //Delete user favourites //Cascade delete
            return base.Delete(id);
        }
        public UserModel FindByUserName(string username)
            => Collection.Find(user => user.Username == username).FirstOrDefault();

        public UserFavouriteRoomsDTO GetUserFavouritesRooms(string id)
        {
            var favs = _favouriteService.Collection.Find(fav => fav.Owner == id).ToEnumerable();
            var user = Read(id);
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
        public IEnumerable<UserModel> GetUsersInRoom(string id)
        {
            return from res in _reservationService.Collection.AsQueryable()
                   where res.Target == id && res.Status == Reservation.Statuses.CHECKIN
                   select Read(res.Owner);
        }
    }
}
