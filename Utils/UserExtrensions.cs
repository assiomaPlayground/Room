using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Utils
{
    public static class ExtensionMethods
    {
        public static IEnumerable<UserModel> WithoutPasswords(this IEnumerable<UserModel> users)
            => users.Select(x => x.WithoutPassword());
        

        public static UserModel WithoutPassword(this UserModel user)
        {
            user.Password = null;
            return user;
        }
    }
}
