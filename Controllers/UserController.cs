using Microsoft.AspNetCore.Mvc;
using RoomService.Models;
using RoomService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomService.Utils;
using RoomService.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RoomService.Controllers
{
    /// <summary>
    /// Controller for User collection base Crud in abstract class
    /// </summary>
    public class UserController : AbstractMongoCrudController<UserModel, UserService>
    {
        private readonly AccessControlService _acs;
        public UserController(UserService service, AccessControlService acs) : base(service) 
        {
            this._acs = acs;
        }
        /// <summary>
        /// Secure user data get
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>the user hiding password</returns>
        public override ActionResult<UserModel> Read([FromRoute] string id)
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            if (!CanRead(rid, id))
                return Forbid();

            var item = Service.Read(id).WithoutPassword().WithoutToken();

            if (item == null)
                return NotFound();
            return item;
        }
        public override ActionResult<IEnumerable<UserModel>> GetAll() 
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            if (!CanReadAll(rid))
                return Forbid();
            var res = Service.GetAll().WithoutPasswords().WithoutTokens();
            if (res == null)
                return BadRequest();
            return new OkObjectResult(res);
        }
        public override IActionResult Create([FromBody] UserModel model)
            => base.Create(model);
        [AllowAnonymous]
        [HttpPost("Token")]
        public UserModel GenerateToken([FromBody] AuthDTO model)
            => Service.Login(model).WithoutPassword();
        [AllowAnonymous]
        [HttpPost("Registration")]
        public ActionResult<UserModel> Registration([FromBody] UserModel model)
        {
            if (!_acs.CanCreateUser(null, model))
                return Forbid();

            return new OkObjectResult(Service.Register(model).WithoutPassword());
        }
        protected override bool CanCreate(string id, UserModel model)
            => _acs.CanCreateUser(id, model);
        protected override bool CanRead(string id, string tid)
            => _acs.IsAuth(id);
        protected override bool CanUpdate(string id, UserModel model)
            => _acs.IsOwner<UserModel>(id, model);
        protected override bool CanDelete(string id, string tid)
            => _acs.IsOwner<UserService, UserModel>(id, tid, Service);
        protected override bool CanReadAll(string id)
            => _acs.IsAuth(id);
    }
}