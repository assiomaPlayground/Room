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
        /// acs as Access
        /// </summary>
        private readonly AccessControlService _acs;
        public WorkSpaceController(WorkSpaceService service, AccessControlService acs) : base(service)
        {
           
            this._acs = acs;
        }
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override bool CanCreate(string id, WorkSpace model)
            => _acs.IsAuth(id);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        protected override bool CanDelete(string id, string tid)
            => _acs.IsAdmin(id);
        
        /// <summary>
        /// Read
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        protected override bool CanRead(string id, string tid)
            => _acs.IsAuth(id);

        /// <summary>
        /// Read All
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected override bool CanReadAll(string id)
            => _acs.IsAuth(id);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override bool CanUpdate(string id, WorkSpace model)
            => _acs.IsAdmin(id);
    }
}
