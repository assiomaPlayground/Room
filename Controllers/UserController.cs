using Microsoft.AspNetCore.Mvc;
using RoomService.Models;
using RoomService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : AbstractMongoCrudController<UserModel, UserService>
    {
        public UserController(UserService service) : base(service) { }
    }
}