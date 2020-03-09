using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Settings
{
    /// <summary>
    /// Interface for mongo database connection
    /// </summary>
    public interface IMongoSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName     { get; set; }
    }
}
