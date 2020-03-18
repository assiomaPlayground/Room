using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{
    /// <summary>
    /// Login data
    /// </summary>
    public class AuthDTO
    {

        /// <summary>
        /// Username
        /// </summary>
        [Required]
        public string Username { get; set; }
        
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
