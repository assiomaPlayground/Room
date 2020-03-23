using Microsoft.AspNetCore.Mvc;
using RoomService.DTO;
using RoomService.Models;
using RoomService.Models.Types;
using RoomService.Services;
using System;
using System.Security.Claims;

namespace RoomService.Controllers
{
    /// <summary>
    /// Controller for building collection base Crud in Abstract Class
    /// </summary>
    public class BuildingController : AbstractMongoCrudController<Building, BuildingService>
    {

        
        

        private readonly AccessControlService _acs;

        /// <summary>
        /// Building of Controller
        /// </summary>
        /// <param name="service">Building Service</param>
        /// <param name="acs">Access Control Service</param>
        public BuildingController(BuildingService service, AccessControlService acs) : base(service)
        {
            this._acs = acs;
        }

        /// <summary>
        /// Spaces of Buildinn
        /// </summary>
        /// <param name="id">lenght 24</param>
        /// <returns>Forbid, not Found</returns>


        [HttpGet("{id:length(24)}/rooms")]
        public ActionResult<BuildingWorkSpaceDTO> GetBuildingSpaces([FromRoute] string id)
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            if (!CanRead(rid, id))
                return Forbid();

            var item = Service.GetBuildingWorkSpace(id);

            if (item == null)
                return NotFound();
            return item;
        }

        /// <summary>
        /// Availability of Check
        /// </summary>
        /// <param name="id">string id</param>
        /// <param name="ms1"></param>
        /// <param name="ms2"></param>
        /// <returns>Forbid, not Found</returns>
        [HttpGet("CheckAvailability/{id:length(24)}/{ms1}/{ms2}")]
        public ActionResult<BuildingWorkSpaceDTO> CheckAvailability
        (
            [FromRoute] string id, 
            [FromRoute] long ms1, [FromRoute] long ms2
        )
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            var interval = new DeltaTime
            {
                StartTime = new DateTime(ms1).ToString("o"),
                EndTime = new DateTime(ms2).ToString("o")
            };
            if (!CanRead(rid, id))
                return Forbid();
            var item = Service.GetAvailableBuildingWorkSpace(rid, id, interval);
            if (item == null)
                return NotFound();
            return item;
        }

        /// <summary>
        /// Can Create
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override bool CanCreate(string id, Building model)
            => _acs.IsAdmin(id);

        /// <summary>
        /// Can Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        protected override bool CanDelete(string id, string tid)
            => _acs.IsAdmin(id);

        /// <summary>
        /// Can Read
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        protected override bool CanRead(string id, string tid)
            => _acs.IsAuth(id);

        /// <summary>
        /// Can Real All
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected override bool CanReadAll(string id)
            => _acs.IsAuth(id);

        /// <summary>
        /// Can Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override bool CanUpdate(string id, Building model)
            => _acs.IsAdmin(id);
    }
}
