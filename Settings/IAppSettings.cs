using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Settings
{
    public interface IAppSettings
    {
        string Secret { get; set; }
    }
}
