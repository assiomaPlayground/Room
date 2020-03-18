using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Utils
{
    public class ResponseWrapper<TModel>
    {
        public ResponseMeta Meta { get; set; }
        public TModel Payload { get; set; }
    }
}
