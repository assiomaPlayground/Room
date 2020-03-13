using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Settings
{
    public interface IAppSettings
    {
        string Secret { get; set; }
        string PasswordHash { get; set; }
        string SaltKey { get; set; }
        string VIKey { get; set; }
        double TokenDuration { get; set; }
    }
}
