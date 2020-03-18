using RoomService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Settings
{
    /// <summary>
    /// Compatibility Interface for App Settings
    /// <see cref="AppSettings"/>
    /// </summary>
    public interface IAppSettings
    {

        /// <summary>
        /// Secret
        /// </summary>
        string Secret { get; set; }

        /// <summary>
        /// Password Hash String (PasswordHash)
        /// </summary>
        string PasswordHash { get; set; }

        /// <summary>
        /// Password Hash Salt Key
        /// </summary>
        string SaltKey { get; set; }

        /// <summary>
        /// Password Hash VI Key
        /// </summary>
        string VIKey { get; set; }

        /// <summary>
        /// Password Hash VI Key
        /// </summary>
        double TokenDuration { get; set; }

        /// <summary>
        /// Clock based Server Tasks Array
        /// </summary>
        ServerTimeTaskData[] ServerTasks { get; set;}
    }
}
