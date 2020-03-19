using Microsoft.AspNetCore.Mvc;
using RoomService.Models;
using RoomService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoomService.Controllers
{
    /// <summary>
    /// Controller for workspace collection base Crud in abstract class
    /// </summary>
    public class WorkSpaceController : AbstractMongoCrudController<WorkSpace, WorkSpaceService>
    {

        /// <summary>
        /// acs as AccessControlService
        /// </summary>
        private readonly AccessControlService _acs;

        /// <summary>
        /// WorkSpaceController
        /// </summary>
        /// <param name="service">WorkSpaceService</param>
        /// <param name="acs">AccessControlService</param>
        public WorkSpaceController(WorkSpaceService service, AccessControlService acs) : base(service)
        {
           
            this._acs = acs;
        }
        /// <summary>
        /// Can Create
        /// </summary>
        /// <param name="id">String id</param>
        /// <param name="model">WorkSpace model</param>
        /// <returns>acs is Auth(id)</returns>
        protected override bool CanCreate(string id, WorkSpace model)
            => _acs.IsAuth(id);

        /// <summary>
        /// Can Delete
        /// </summary>
        /// <param name="id">String id</param>
        /// <param name="tid">String tid</param>
        /// <returns>acs Is Admin(id)</returns>
        protected override bool CanDelete(string id, string tid)
            => _acs.IsAdmin(id);
        
        /// <summary>
        /// Can Read
        /// </summary>
        /// <param name="id">String id</param>
        /// <param name="tid">String tid</param>
        /// <returns>acs Is Auth(id)</returns>
        protected override bool CanRead(string id, string tid)
            => _acs.IsAuth(id);

        /// <summary>
        /// Can Read All
        /// </summary>
        /// <param name="id">String id</param>
        /// <returns>acs is Auth(id)</returns>
        protected override bool CanReadAll(string id)
            => _acs.IsAuth(id);

        /// <summary>
        /// Can Update
        /// </summary>
        /// <param name="id">String id</param>
        /// <param name="model">Workspace model</param>
        /// <returns>acs Is Admin(id)</returns>
        protected override bool CanUpdate(string id, WorkSpace model)
            => _acs.IsAdmin(id);
    }
}
