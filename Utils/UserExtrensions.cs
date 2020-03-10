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
        public static IEnumerable<UserModel> WithoutTokens(this IEnumerable<UserModel> users)
            => users.Select(x => x.WithoutToken());

        public static UserModel WithoutPassword(this UserModel user)
        {
            user.Password = null;
            return user;
        }
        public static UserModel WithoutToken(this UserModel user)
        {
            user.Token = null;
            return user;
        }
    }
}
