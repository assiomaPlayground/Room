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
        public UserService(IRoomServiceMongoSettings settings, 
            IAppSettings appSettings, 
            CrypProvider cryptProvider,
            ReservationService reservationService,
            FavouritesService favouritesService
            )
        {
            base.Init(settings, settings.UserCollection);
            this._sectet = appSettings.Secret;
            this._cryptProvider = cryptProvider;
            this._TokenLifetime = appSettings.TokenDuration;

            this._reservationService = reservationService;
            this._favouriteService = favouritesService;
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
                     user.Username == authData.Username &&
                     user.Password == authData.Password
                ).FirstOrDefault<UserModel>();
            if (user == null) return null;

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
    }
}
