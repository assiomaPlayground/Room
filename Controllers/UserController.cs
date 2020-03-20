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
        /// <summary>
        /// acs AccessControlService
        /// </summary>
        private readonly AccessControlService _acs;
        /// <summary>
        /// UserController
        /// </summary>
        /// <param name="service">service UserService</param>
        /// <param name="acs">acs AccessControlService</param>
        public UserController(UserService service, AccessControlService acs) : base(service) 
        {
            this._acs = acs;
        }
        /// <summary>
        /// Find an user
        /// </summary>
        /// <param name="id">The user id : 24 string</param>
        /// <returns>if null result not Found, new ok Object Result</returns>
        [HttpGet("Find/{id:length(24)}")]
        public ActionResult<FoundUserWorkSpaceDTO> FindUserLocation([FromRoute] string id)
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            if (!CanRead(rid, id))
                return Forbid();
            var res = Service.FindUserLocationById(id);
            if (res == null)
                return NotFound();
            return new OkObjectResult(res);
        }
        /// <summary>
        /// In Work Space
        /// </summary>
        /// <param name="id">The id : 24 string</param>
        /// <returns>if null result not Found, new ok Object Result</returns>
        [HttpGet("InWorkSpace/{id:length(24)}")]
        public ActionResult<IEnumerable<UserModel>> GetUserInWorkSpace([FromRoute] string id)
        {   
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            if (!CanRead(rid, id))
                return Forbid();
            var res = Service.GetUserInWorkSpace(id).WithoutPasswords();
            if (res == null)
                return NotFound();
            return new OkObjectResult(res);
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
        /// <summary>
        /// Secure user data get
        /// </summary>
        /// <returns>if null Bad Request, new Ok Object Result</returns>
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
        /// <summary>
        /// Token Generation request
        /// </summary>
        /// <param name="model">Login data model</param>
        /// <returns>Generated token</returns>
        [AllowAnonymous]
        [HttpPost("Token")]
        public UserModel GenerateToken([FromBody] AuthDTO model)
            => Service.Login(model).WithoutPassword();
        /// <summary>
        /// Registration Request
        /// </summary>
        /// <param name="model">User model to create</param>
        /// <returns>New created user</returns>
        [AllowAnonymous]
        [HttpPost("Registration")]
        public ActionResult<UserModel> Registration([FromBody] UserModel model)
        {
            if (!model.IsValid())
                BadRequest();
            if (!_acs.CanCreateUser(null, model))
                return Forbid();

            return new OkObjectResult(Service.Register(model).WithoutPassword());
        }
        /// <summary>
        /// Gets the user favourite workspaces
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>The user favorurite WorkSpaces</returns>
        [HttpGet("Favourites/{id:length(24)}")]
        public ActionResult<UserFavouriteWorkSpaceDTO> GetUserFavouritesRooms([FromRoute] string id)
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            if ( rid != id  || !_acs.IsAdmin(id)) //@TODO Move method into acs
                return Forbid();
            var res = Service.GetUserFavouriteWorkSpace(id);
            if (res == null)
                return NotFound();
            return new OkObjectResult(res);
        }
        //Access control base rules
        /// <summary>
        /// Can Create
        /// </summary>
        /// <param name="id">String id</param>
        /// <param name="model">model UserModel</param>
        /// <returns>acs Can Create User(id,model)</returns>
        protected override bool CanCreate(string id, UserModel model)
            => _acs.CanCreateUser(id, model);
        /// <summary>
        /// Can Read
        /// </summary>
        /// <param name="id">String id</param>
        /// <param name="tid">String tid</param>
        /// <returns>acs Is Auth(id)</returns>
        protected override bool CanRead(string id, string tid)
            => _acs.IsAuth(id);
        /// <summary>
        /// Can Update
        /// </summary>
        /// <param name="id">String id</param>
        /// <param name="model">model UserModel</param>
        /// <returns>acs Is Owner(id,model)</returns>
        protected override bool CanUpdate(string id, UserModel model)
            => _acs.IsOwner<UserModel>(id, model);
        /// <summary>
        /// Can Delete
        /// </summary>
        /// <param name="id">String id</param>
        /// <param name="tid">String tid</param>
        /// <returns>acs Is Owner(id,tid,Service)</returns>
        protected override bool CanDelete(string id, string tid)
            => _acs.IsOwner<UserService, UserModel>(id, tid, Service);
        /// <summary>
        /// Can Read All
        /// </summary>
        /// <param name="id">String id</param>
        /// <returns>acs Is Auth(id)</returns>
        protected override bool CanReadAll(string id)
            => _acs.IsAuth(id);
    }
}