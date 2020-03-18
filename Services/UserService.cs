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
        /// <summary>
        /// Execute an user creation
        /// </summary>
        /// <param name="model">The user data to insert</param>
        /// <returns>The created user in database and his token</returns>
        public UserModel Register(UserModel model)
        {
            this.Create(model);
            return this.Login(new AuthDTO { Password = _cryptProvider.Decrypt(model.Password), Username = model.Username });
        }
        /// <summary>
        /// Create an user
        /// </summary>
        /// <param name="model">The user data to insert</param>
        /// <returns>The created user without token and password</returns>
        public override UserModel Create(UserModel model)
        {
            model.Password = _cryptProvider.Encrypt(model.Password);
            return base.Create(model);
        }
        /// <summary>
        /// Update an user
        /// </summary>
        /// <param name="id">Target User id</param>
        /// <param name="newModel">Ner User data</param>
        /// <returns>Replace result class by mongo driver</returns>
        public override ReplaceOneResult Update(string id, UserModel newModel)
        {
            newModel.Password = _cryptProvider.Encrypt(newModel.Password);
            return base.Update(id, newModel);
        }
        /// <summary>
        /// Execute the login Method
        /// </summary>
        /// <param name="authData">The simplified user model AuthData has only username and password</param>
        /// <returns>The created user with token</returns>
        public UserModel Login(AuthDTO authData)
        {
            //Search for user in db username is a key in db could be indexed
            var user = Collection.Find<UserModel>
                (user =>
                     user.Username == authData.Username 
                ).FirstOrDefault<UserModel>();
            //Fail
            if (user == null) return new UserModel();
            //If user is found check for password
            if (_cryptProvider.Decrypt(user.Password) != authData.Password)
                return new UserModel();
            //@TODO: use a token helper service?
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._sectet);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", user.Id.ToString()) //Save in identity claims the userId
                }),
                Expires = DateTime.UtcNow.AddDays(_TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //Write the generated token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            //return the user
            return user;
        }
        /// <summary>
        /// Delete an user
        /// Could chain a reservation delete and a favourites delete
        /// </summary>
        /// <param name="id">the user to delete</param>
        /// <returns>The delete result mongo driver class</returns>
        public override DeleteResult Delete(string id)
        {
            //@TODO: delete from here
            //_reservationRepo.DeleteByUserId(id); //Delete user reservation //Cascade delete
            //_favouriteRepo.DeleteByUserId(id); //Delete user favourites //Cascade delete
            return base.Delete(id);
        }
        /// <summary>
        /// Find an user by his username
        /// </summary>
        /// <remarks>Username is a key in the database</remarks>
        /// <param name="username">The string of the user to find</param>
        /// <returns>The found User Model</returns>
        public UserModel FindByUserName(string username)
            => Collection.Find(user => user.Username == username).FirstOrDefault();
        /// <summary>
        /// Gets the favourite rooms (WorkSpace) of a target user
        /// </summary>
        /// <param name="id">The target user id</param>
        /// <returns>And user and a collection of his favourites rooms</returns>
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
        /// <summary>
        /// Look for all users inside a WorkSpace using their checkin status
        /// </summary>
        /// <param name="id">The WorkSpace id</param>
        /// <returns>IEnumerable of found users</returns>
        public IEnumerable<UserModel> GetUsersInRoom(string id)
        {
            return from res in _reservationRepo.AsQueryable()
                   where res.Target == id && res.Status == Reservation.Statuses.CHECKIN
                   select Read(res.Owner);
        }
    }
}
