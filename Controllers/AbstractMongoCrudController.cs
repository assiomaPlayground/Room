using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomService.Models;
using RoomService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoomService.Controllers
{
    /// <summary>
    /// Abstract Crud Controller crud ops are virtual
    /// </summary>
    /// <typeparam name="TModel">A Target class model type</typeparam>
    /// <typeparam name="TService">Service type that carries out the Crud ops</typeparam>
    [Authorize] 
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AbstractMongoCrudController<TModel, TService> : ControllerBase, IMongoCrudController<TModel>
        where TModel : class, IModel
        where TService : AbstractMongoCrudService<TModel>
    {
        /// <summary>
        /// Service ref
        /// </summary>
        protected TService Service { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service">Injected service</param>
        public AbstractMongoCrudController(TService service)
            => this.Service = service;

        /// <summary>
        /// Create op
        /// </summary>
        /// <param name="model">Json serialized TModel type in Body</param>
        [HttpPost]
        public virtual ActionResult<TModel> Create([FromBody] TModel model)
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            if (!CanCreate(rid, model))
                return Forbid();
            model = Service.Create(model);
            return new OkObjectResult(model);
        }
        /// <summary>
        /// Delete op
        /// </summary>
        /// <param name="id">The id : 24 string to delete</param>
        /// <returns>True : success,  false : else</returns>
        [HttpDelete("{id:length(24)}")]
        public virtual IActionResult Delete(string id)
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            if (!CanDelete(rid, id))
                return Forbid();
            if (Service.Delete(id).IsAcknowledged)
                return Ok();
            return BadRequest();
        }
        /// <summary>
        /// get all op
        /// @TODO pagination or result limit
        /// </summary>
        /// <returns>ICollection<TModel> (List) eventually 0 sized</returns>
        [HttpGet]
        public virtual ActionResult<IEnumerable<TModel>> GetAll()
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            if (!CanReadAll(rid))
                return Forbid();
            var res = Service.GetAll();
            if (res == null)
                return NotFound();
            return new OkObjectResult(res);
        }
        /// <summary>
        /// get op
        /// </summary>
        /// <param name="id">The id : 24 string to Read</param>
        /// <returns> The json serialized object eventually default</returns>
        [HttpGet("{id:length(24)}")]
        public virtual ActionResult<TModel> Read([FromRoute] string id)
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            if (!CanRead(rid, id))
                return Forbid();
            var item = Service.Read(id);
            if (item == null)
                return NotFound();
            return item;
        }
        /// <summary>
        /// Update op
        /// </summary>
        /// <param name="id"> Target resource id</param>
        /// <param name="model"> the new Json serialized TModel type in Body</param>
        /// <returns>True : success, false : else</returns>
        [HttpPut("{id:length(24)}")]
        public virtual IActionResult Update([FromRoute] string id, [FromBody] TModel model)
        {
            var rid = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("userId").Value;
            if (!CanUpdate(rid, model))
                return Forbid();
            if (Service.Update(id, model).IsAcknowledged)
                return Ok();
            return BadRequest();
        }
        //Crud Access Controls

            /// <summary>
            /// Can Create
            /// </summary>
            /// <param name="id"></param>
            /// <param name="model"></param>
            /// <returns></returns>
        protected abstract bool CanCreate(string id, TModel model);

        /// <summary>
        /// Can Read
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        protected abstract bool CanRead(string id, string tid);

        /// <summary>
        /// Can Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected abstract bool CanUpdate(string id, TModel model);

        /// <summary>
        /// Can Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        protected abstract bool CanDelete(string id, string tid);

        /// <summary>
        /// Can Read All
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected abstract bool CanReadAll(string id);
    }
}
