using Microsoft.AspNetCore.Mvc;
using RoomService.Model;
using RoomService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Controllers
{
    public class QRMapController : AbstractMongoCrudController<QRMap, QRMapService>
    {
        public QRMapController(QRMapService service) : base(service) { }
    }
}
