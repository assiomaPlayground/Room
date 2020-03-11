using Microsoft.AspNetCore.Mvc;
using RoomService.Models;
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
        private readonly AccessControlService _acs;
        public WorkSpaceController(WorkSpaceService service, AccessControlService acs) : base(service) 
        {
            this._acs = acs;
        }
        protected override bool CanCreate(string id, WorkSpace model)
            => _acs.IsAdmin(id);
        protected override bool CanDelete(string id, string tid)
            => _acs.IsAdmin(id);
        protected override bool CanRead(string id, string tid)
            => _acs.IsAuth(id);
        protected override bool CanReadAll(string id)
            => _acs.IsAuth(id);
        protected override bool CanUpdate(string id, WorkSpace model)
            => _acs.IsAdmin(id);
    }
}
