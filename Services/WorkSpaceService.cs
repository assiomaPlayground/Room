using RoomService.Models;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Services
{
    /// <summary>
    /// Service for WorSpace collection crud ops in abstract
    /// </summary>
    public class WorkSpaceService : AbstractMongoCrudService<WorkSpace>
    {
        public WorkSpaceService(IRoomServiceMongoSettings settings)
            => base.Init(settings, settings.WorkSpaceCollection);
    }
    
}
