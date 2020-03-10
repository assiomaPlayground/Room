using Microsoft.AspNetCore.Mvc;
using RoomService.Models;
using RoomService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomService.Utils;
using RoomService.DTO;

namespace RoomService.Controllers
{
    /// <summary>
    /// Controller for User collection base Crud in abstract class
    /// </summary>
    public class UserController : AbstractMongoCrudController<UserModel, UserService>
    {
        public UserController(UserService service) : base(service) { }
        /// <summary>
        /// Secure user data get
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>the user hiding password</returns>
        [HttpGet("{id:length(24)}")]
        public override UserModel Read([FromRoute] string id)
            => base.Read(id).WithoutPassword();

        public override IEnumerable<UserModel> GetAll()
            => base.GetAll().WithoutPasswords();

        [HttpPost("Token")]
        public UserModel GenerateToken([FromBody] AuthDTO model)
            => Service.Login(model);
    }
}