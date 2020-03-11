using RoomService.Models;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Services
{
    /// <summary>
    /// Service for QRMap collection crud ops in abstract
    /// </summary>
    public class QRMapService : AbstractMongoCrudService<QRMap>
    {
        public QRMapService(IRoomServiceMongoSettings settings)
            => base.Init(settings, settings.QRMapCollection);
    }
}
