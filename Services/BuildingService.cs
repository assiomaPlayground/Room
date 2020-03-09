using RoomService.Model;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Services
{
    public class BuildingService : AbstractMongoCrudService<Building>
    {
        public BuildingService(IRoomServiceMongoSettings settings)
            => base.Init(settings, settings.BuildingCollection);
    }
}
