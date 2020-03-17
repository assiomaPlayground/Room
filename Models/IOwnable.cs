using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Models
{
    public interface IOwnable
    {

        //Owner
        public string Owner { get; }
    }
}
