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
    /// Controller for QRMap collection base Crud in abstract class
    /// </summary>
    public class QRMapController : AbstractMongoCrudController<QRMap, QRMapService>
    {
        public QRMapController(QRMapService service) : base(service) { }
    }
}
