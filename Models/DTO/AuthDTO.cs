﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.DTO
{
    /// <summary>
    /// Login data with Username and Password
    /// </summary>
    public class AuthDTO
    {
        /// <summary>
        /// String Username
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// String Password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
