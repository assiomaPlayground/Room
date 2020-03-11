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
    /// Controller for QRMap collection base Crud in abstract class
    /// </summary>
    public class QRMapController : AbstractMongoCrudController<QRMap, QRMapService>
    {
        private readonly AccessControlService _acs;
        public QRMapController(QRMapService service, AccessControlService acs) : base(service) 
        {
            this._acs = acs;
        }
        protected override bool CanCreate(string id, QRMap model)
            => _acs.IsAdmin(id);
        protected override bool CanDelete(string id, string tid)
            => _acs.IsAdmin(id);
        protected override bool CanRead(string id, string tid)
            => _acs.IsAuth(id);
        protected override bool CanReadAll(string id)
            => _acs.IsAuth(id);
        protected override bool CanUpdate(string id, QRMap model)
            => _acs.IsAdmin(id);
    }
}
