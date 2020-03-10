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
        private readonly string _sectet;
        private readonly RSAProvider _rsaProvider;
        public UserService(IRoomServiceMongoSettings settings, IAppSettings appSettings, RSAProvider rsaProvider)
        {
            base.Init(settings, settings.UserCollection);
            this._sectet = appSettings.Secret;
            this._rsaProvider = rsaProvider;
        }

        public override void Create(UserModel model)
        {
            model.Password = _rsaProvider.Encrypt(model.Password);
            base.Create(model);
        }

        public override bool Update(string id, UserModel newModel)
        {
            newModel.Password = _rsaProvider.Encrypt(newModel.Password);
            return base.Update(id, newModel);
        }

        public UserModel Login(AuthDTO authData)
        {
            authData.Password = _rsaProvider.Encrypt(authData.Password);
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
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            
            return user;
        }
    }
}
