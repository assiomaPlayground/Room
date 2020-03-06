using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Settings
{
    public interface IMongoSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName     { get; set; }
        string BaseCollection   { get; set; }
    }
}
