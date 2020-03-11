using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Settings
{
    public class AppSettings : IAppSettings
    {
        public string Secret { get; set; }
        public double TokenDuration { get; set; }
    }
}
