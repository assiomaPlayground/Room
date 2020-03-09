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
    /// <summary>
    /// Controller for building collection base Crud in abstract class
    /// </summary>
    public class BuildingController : AbstractMongoCrudController<Building, BuildingService>
    {
        public BuildingController(BuildingService service) : base(service) { }
    }
}
