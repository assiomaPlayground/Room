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
        /// Needed repository for aggregation operations with WorkSpace reservation
        /// </summary>
        private readonly IMongoCollection<WorkSpaceReservation> _workSpaceReservationRepo;
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
        /// <param name="appSettings">The whole app settings wrapper</param>
        /// <param name="cryptProvider">The utility service for password encrypt</param>
        public UserService(IRoomServiceMongoSettings settings, IAppSettings appSettings, CrypProvider cryptProvider)        
        {
            //Init base
            base.Init(settings, settings.UserCollection);
            //Set Encryption Data
            this._sectet = appSettings.Secret;
            this._cryptProvider = cryptProvider;
            this._TokenLifetime = appSettings.TokenDuration;
            //Gets extra collections
            this._reservationRepo = Database.GetCollection<Reservation>(settings.ReservationCollection);
            this._favouriteRepo   = Database.GetCollection<Favourites> (settings.FavouritesCollection);
            this._workSpaceRepo   = Database.GetCollection<WorkSpace>  (settings.WorkSpaceCollection);
            this._workSpaceReservationRepo = Database.GetCollection<WorkSpaceReservation>(settings.WorkSpaceReservationCollection);
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
            //Store data in token
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
            DeleteAllUserReservationById(id);
            _favouriteRepo.DeleteMany(fav => fav.Owner == id);
            return base.Delete(id);
        }
        /// <summary>
        /// Delete user Reservation
        /// Chains a seat free in ref WorkSpaceReservation
        /// </summary>
        /// <param name="id">The user Id</param>
        /// <returns>The delete result mongo driver instance</returns>
        public DeleteResult DeleteAllUserReservationById(string id)
        {
            //Get candidates
            var allRes = _reservationRepo.Find(anyRes => anyRes.Owner == id).ToList();
            foreach (var res in allRes)
            {
                //Free user seats
                var seats = _workSpaceReservationRepo.Find(wrkRes => wrkRes.Id == res.ReservationSocket).ToList(); //Should be 1
                foreach (var seat in seats)
                {
                    seat.Reservations--;
                    _workSpaceReservationRepo.ReplaceOne(wrkRes => wrkRes.Id == seat.Id, seat);
                }
            }
            //Return resuls
            return _reservationRepo.DeleteMany(anyRes => anyRes.Owner == id);
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
        /// Gets the favourite WorkSpace of a target user
        /// </summary>
        /// <param name="id">The target user id</param>
        /// <returns>And user and a collection of his favourites WorkSpace</returns>
        public UserFavouriteWorkSpaceDTO GetUserFavouriteWorkSpace(string id)
        {
            var favs = _favouriteRepo.Find(fav => fav.Owner == id).ToEnumerable();
            var user = Read(id).WithoutPassword();
            var qres = from fav in favs.AsQueryable()
                       join workspc in _workSpaceRepo.AsQueryable() on fav.Target equals workspc.Id
                       select new UserFavouriteWorkSpaceDTO.FavouriteWorkSpaceDTO
                       {
                           Favourite = fav,
                           WorkSpace = workspc
                       };
            return new UserFavouriteWorkSpaceDTO { User = user, Favourites = qres.ToArray() };
        }
        /// <summary>
        /// Look for all users inside a WorkSpace using their checkin status
        /// </summary>
        /// <param name="id">The WorkSpace id</param>
        /// <returns>IEnumerable of found users</returns>
        public IEnumerable<UserModel> GetUserInWorkSpace(string id)
        {
            return from res in _reservationRepo.AsQueryable()
                   where res.Target == id && res.Status == Reservation.Statuses.CHECKIN
                   select Read(res.Owner);
        }
        /// <summary>
        /// Uses the reservation as "middleman" for join workspace and availability in reservation interval
        /// </summary>
        /// <param name="id">The user Id</param>
        /// <returns>Joined reservation workspace and availability in the interval DeltaTime of the requested user reservations</returns>
        public FoundUserWorkSpaceDTO FindUserLocationById(string id)
        {
            //Get the user
            var user = Read(id);
            if (user == null)
                return null;
            //Get user reservation checked in //Should be only 1 at time
            var res = _reservationRepo.Find(res => res.Owner == id && res.Status == Reservation.Statuses.CHECKIN).FirstOrDefault();
            //Not found
            if (res == null)
                return null;
            //Get WorkSpace
            var wrkSp = _workSpaceRepo.Find(wrk => wrk.Id == res.Target).FirstOrDefault();
            if (wrkSp == null)
                return null;
            //Get WorkSpaceReservation
            var wrkRes = _workSpaceReservationRepo.Find(wrkres => wrkres.Id == res.ReservationSocket).FirstOrDefault();
            if (wrkRes == null)
                return null;
            //Return result
            return new FoundUserWorkSpaceDTO { User = user.WithoutPassword(), 
                WorkSpaceReservation = new WorkSpaceReservationDTO 
                { Users = wrkRes.Reservations, Interval = res.Interval, WorkSpace = wrkSp } 
            };
        }
    }
}
