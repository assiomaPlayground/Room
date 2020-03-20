using RoomService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Settings
{
    /// <summary>
    /// Global App Settings class wrapper for config json data
    /// </summary>
    public class AppSettings : IAppSettings
    {
        /// <summary>
        /// Used secret for Token Generation
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// Token expire time duration
        /// </summary>
        public double TokenDuration { get; set; }
        /// <summary>
        /// Password Hash string (PasswordHash)
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// Password Hash Salt Key
        /// </summary>
        public string SaltKey { get; set; }
        /// <summary>
        /// Password Hash VI Key
        /// </summary>
        public string VIKey { get; set; }
        /// <summary>
        /// Clock based Server Tasks Array
        /// </summary>
        public ServerTimeTaskData[] ServerTasks { get; set; }
    }
}
