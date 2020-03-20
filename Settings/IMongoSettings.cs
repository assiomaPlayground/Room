using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Settings
{
    /// <summary>
    /// Interface for Mongo database connection
    /// <see cref="RoomServiceMongoSettings"/>
    /// </summary>
    public interface IMongoSettings
    {
        /// <summary>
        /// Connection
        /// </summary>
        string ConnectionString { get; set; }
        /// <summary>
        /// Name of the Database where to connect
        /// </summary>
        string DatabaseName { get; set; }
    }
}
