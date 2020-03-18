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
    /// </summary>
    public class UserService : AbstractMongoCrudService<UserModel>
    {
        /// <summary>
        /// The token secret got by settings
        /// </summary>
        private readonly string _sectet;
        /// <summary>
        /// The token lifetime got by settings
        /// </summary>
        private readonly double _TokenLifetime;
        /// <summary>
        /// The required crypt provider helper service for password decryption/encryption got by DI
        /// </summary>
        private readonly CrypProvider _cryptProvider;
        /// <summary>
        /// Needed repository for aggregation operations with Reservations
        /// </summary>
        private readonly IMongoCollection<Reservation> _reservationRepo;
        /// <summary>
        /// Needed repository for aggregation operations with Favourites
        /// </summary>
        private readonly IMongoCollection<Favourites>  _favouriteRepo;
        /// <summary>
        /// Needed repository for aggregation operations with WorkSpace
        /// </summary>
        private readonly IMongoCollection<WorkSpace>   _workSpaceRepo;
        /// <summary>
        /// Constructor sets the DI
        /// </summary>
        /// <param name="settings">The required settings</param>
        public UserService(IRoomServiceMongoSettings settings, IAppSettings appSettings, CrypProvider cryptProvider)        
        {
            base.Init(settings, settings.UserCollection);

            this._sectet = appSettings.Secret;
            this._cryptProvider = cryptProvider;
            this._TokenLifetime = appSettings.TokenDuration;

            this._reservationRepo = Database.GetCollection<Reservation>(settings.ReservationCollection);
            this._favouriteRepo   = Database.GetCollection<Favourites> (settings.FavouritesCollection);
            this._workSpaceRepo   = Database.GetCollection<WorkSpace>  (settings.WorkSpaceCollection);
        }

        public UserModel Register(UserModel model)
        {
            this.Create(model);
            return this.Login(new AuthDTO { Password = _cryptProvider.Decrypt(model.Password), Username = model.Username });
        }
        public override UserModel Create(UserModel model)
        {
            model.Password = _cryptProvider.Encrypt(model.Password);
            return base.Create(model);
        }
        public override ReplaceOneResult Update(string id, UserModel newModel)
        {
            newModel.Password = _cryptProvider.Encrypt(newModel.Password);
            return base.Update(id, newModel);
        }
        public UserModel Login(AuthDTO authData)
        {
            
            var user = Collection.Find<UserModel>
                (user =>
                     user.Username == authData.Username 
                ).FirstOrDefault<UserModel>();
            if (user == null) return new UserModel();
            if (_cryptProvider.Decrypt(user.Password) != authData.Password)
                return new UserModel();
            //@TODO: use a token helper service?
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
            //_reservationRepo.DeleteByUserId(id); //Delete user reservation //Cascade delete
            //_favouriteRepo.DeleteByUserId(id); //Delete user favourites //Cascade delete
            return base.Delete(id);
        }
        public UserModel FindByUserName(string username)
            => Collection.Find(user => user.Username == username).FirstOrDefault();

        public UserFavouriteRoomsDTO GetUserFavouritesRooms(string id)
        {
            var favs = _favouriteRepo.Find(fav => fav.Owner == id).ToEnumerable();
            var user = Read(id).WithoutPassword();
            var qres = from fav in favs.AsQueryable()
                       join room in _workSpaceRepo.AsQueryable() on fav.Target equals room.Id
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
            return from res in _reservationRepo.AsQueryable()
                   where res.Target == id && res.Status == Reservation.Statuses.CHECKIN
                   select Read(res.Owner);
        }
    }
}
