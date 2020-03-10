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
        public UserService(IRoomServiceMongoSettings settings, IAppSettings appSettings)
        {
            base.Init(settings, settings.UserCollection);
            this._sectet = appSettings.Secret;
        }
        public UserModel Login(AuthDTO authData)
        {
            var user = Collection.Find<UserModel>
                (user =>
                     user.Username == authData.Username &&
                     user.Password == authData.Password
                ).First<UserModel>();
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

            this.Update(user.Id, user);
            
            return user.WithoutPassword();
        }
    }
}
