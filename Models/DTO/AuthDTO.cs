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

        //Username
        [Required]
        public string Username { get; set; }
        
        //Password
        [Required]
        public string Password { get; set; }
    }
}
