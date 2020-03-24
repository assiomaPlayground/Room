using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Utils
{
    /// <summary>
    /// Container for dynamic add response meta to service data
    /// </summary>
    /// <typeparam name="TModel">Base type of response</typeparam>
    public class ResponseWrapper<TModel>
    {
        /// <summary>
        /// The response meta data
        /// </summary>
        public ResponseMeta Meta { get; set; }

        /// <summary>
        /// The payload data
        /// </summary>
        public TModel Payload { get; set; }
    }
}
