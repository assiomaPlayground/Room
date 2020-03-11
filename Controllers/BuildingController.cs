using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomService.DTO;
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
    /// Controller for building collection base Crud in abstract class
    /// </summary>
    [Authorize]
    public class BuildingController : AbstractMongoCrudController<Building, BuildingService>
    {
        private readonly AccessControlService _acs;
        public BuildingController(BuildingService service, AccessControlService acs) : base(service) 
        {
            this._acs = acs;
        }
        [HttpGet("{id:length(24)}/rooms")]
        public ActionResult<BuildingSpacesDTO> GetBuildingSpaces([FromRoute] string id)
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            if (!CanRead(rid, id))
                return Forbid();

            var item = Service.GetBuildingSpaces(id);

            if (item == null)
                return NotFound();
            return item;
        }
        protected override bool CanCreate(string id, Building model)
            => _acs.IsAdmin(id);
        protected override bool CanDelete(string id, string tid)
            => _acs.IsAdmin(id);
        protected override bool CanRead(string id, string tid)
            => _acs.IsAuth(id);
        protected override bool CanReadAll(string id)
            => _acs.IsAuth(id);
        protected override bool CanUpdate(string id, Building model)
            => _acs.IsAdmin(id);
    }
}
