using Microsoft.AspNetCore.Mvc;
using RoomService.Model;
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
    public class BuildingController : AbstractMongoCrudController<Building, BuildingService>
    {
        public BuildingController(BuildingService service) : base(service) { }
    }
}
