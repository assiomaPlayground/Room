using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Utils
{
    /// <summary>
    /// Used http response enum
    /// </summary>
    public enum ResponseTypes
    {

        /// <summary>
        /// Success status 200
        /// </summary>
        Ok,
        /// <summary>
        /// No resouce 404
        /// </summary>
        NotFound,
        /// <summary>
        /// Bad input 400
        /// </summary>
        BadRequest,
        /// <summary>
        /// Conflict used for valid input but invalid context 409
        /// </summary>
        Conflict,
        /// <summary>
        /// Insufficient permissions 403
        /// </summary>
        Forbid
    }

    /// <summary>
    /// Metatada response archetype
    /// </summary>
    public class ResponseMeta
    {
        /// <summary>
        /// Http response type
        /// <see cref="ResponseTypes"/>
        /// </summary>
        public ResponseTypes Type { get; set; }
        /// <summary>
        /// Message string for client 
        /// </summary>
        public string Message { get; set; }
    }
}
