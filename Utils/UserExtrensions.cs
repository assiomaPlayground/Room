using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RoomService.Utils
{
    /// <summary>
    /// User class Exstensions
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// A pipe that removes the password from an users IEnumerable
        /// </summary>
        /// <param name="users">The user IEnumerable</param>
        /// <returns>The modified users IEnumerable</returns>
        public static IEnumerable<UserModel> WithoutPasswords(this IEnumerable<UserModel> users)
            => users.Select(x => x.WithoutPassword());
        /// <summary>
        /// A pipe that removes the tokens from an users IEnumerable
        /// @TODO: Delete token are now ignored by BSON and NewSoftJson
        /// </summary>
        /// <param name="users">The user IEnumerable</param>
        /// <returns>The modified users IEnumerable</returns>
        public static IEnumerable<UserModel> WithoutTokens(this IEnumerable<UserModel> users)
            => users.Select(x => x.WithoutToken());
        /// <summary>
        /// A pipe that removes the password from an user
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>The modified user</returns>
        public static UserModel WithoutPassword(this UserModel user)
        {
            user.Password = null;
            return user;
        }
        /// <summary>
        /// A pipe that removes the token from an user
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>The modified user</returns>
        public static UserModel WithoutToken(this UserModel user)
        {
            user.Token = null;
            return user;
        }
        /// <summary>
        /// Validator for UserModel class
        /// <para>
        ///     UserModel valid definition is a user that has username and password matching the respective regular expressions
        /// </para>
        /// </summary>
        /// <param name="user">Target to validate</param>
        /// <returns>The bool that indicates the valid status</returns>
        public static bool IsValid(this UserModel user)
        {
            //Username regular expression matcher
            var usernameRegex = new Regex(@"^[a-zA-Z0-9]([._](?![._])|[a-zA-Z0-9]){4,16}[a-zA-Z0-9]$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase,
                TimeSpan.FromMilliseconds(1000)
            );
            if (!usernameRegex.IsMatch(user.Username)) //Invalid username
                return false;
            //Password regular expression matcher
            var passRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,16}$", 
                RegexOptions.Compiled, 
                TimeSpan.FromMilliseconds(1000)
            ); //Pass validator
            if (!passRegex.IsMatch(user.Password)) //Invalid password
                return false;
            //Success
            return true;
        }
    }
}
