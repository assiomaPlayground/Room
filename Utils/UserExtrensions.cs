using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RoomService.Utils
{
    public static partial class ExtensionMethods
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
        public static bool IsValid(this UserModel user)
        {
            var usernameRegex = new Regex(@"^[a-zA-Z0-9]([._](?![._])|[a-zA-Z0-9]){4,16}[a-zA-Z0-9]$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase,
                TimeSpan.FromMilliseconds(1000)
            );
            if (!usernameRegex.IsMatch(user.Username)) //Invalid username
                return false;

            var passRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,16}$", 
                RegexOptions.Compiled, 
                TimeSpan.FromMilliseconds(1000)
            ); //Pass validator
            if (!passRegex.IsMatch(user.Password)) //Invalid password
                return false;

            return true;
        }
    }
}
