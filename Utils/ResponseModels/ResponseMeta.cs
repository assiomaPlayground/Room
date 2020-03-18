using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Utils
{
    public enum ResponseTypes
    {
        Ok,
        Error,
        Forbid
    }
    public class ResponseMeta
    {
        public ResponseTypes Type { get; set; }
        public string Message { get; set; }
    }
}
