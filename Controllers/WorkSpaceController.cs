using Microsoft.AspNetCore.Mvc;
using RoomService.Model;
using RoomService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Controllers
{
    public class WorkSpaceController : AbstractMongoCrudController<WorkSpace, WorkSpaceService>
    {
        public WorkSpaceController(WorkSpaceService service) : base(service) { }
    }
}
