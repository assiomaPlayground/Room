using RoomService.Models;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Services
{
    public class UserService : AbstractMongoCrudService<UserModel>
    {
        public UserService(IRoomServiceMongoSettings settings)
            => base.Init(settings, settings.UserCollection);
    }
}
