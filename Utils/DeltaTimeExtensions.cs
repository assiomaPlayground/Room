using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomService.Models;
using RoomService.Models.Types;

namespace RoomService.Utils
{
    public static class DeltaTimeExtensions
    {
        public static bool IsValid(this DeltaTime deltaTime)
            => (string.Compare(deltaTime.StartTime, deltaTime.EndTime) < 0);
    }
}
