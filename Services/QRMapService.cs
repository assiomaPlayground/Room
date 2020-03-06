using RoomService.Model;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Services
{
    public class QRMapService : AbstractMongoCrudService<QRMap>
    {
        public QRMapService(IRoomServiceMongoSettings settings)
            => base.Init(settings, "QRMap");
    }
}
