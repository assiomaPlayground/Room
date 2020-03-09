using Microsoft.AspNetCore.Mvc;
using RoomService.Model;
using RoomService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Controllers
{
    /// <summary>
    /// Controller for workspace collection base Crud in abstract class
    /// </summary>
    public class WorkSpaceController : AbstractMongoCrudController<WorkSpace, WorkSpaceService>
    {
        public WorkSpaceController(WorkSpaceService service) : base(service) { }
    }
}
