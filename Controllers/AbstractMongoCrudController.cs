using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomService.Models;
using RoomService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Controllers
{
    /// <summary>
    /// Abstract Crud controller crud ops are virtual
    /// </summary>
    /// <typeparam name="TModel">A target class model type</typeparam>
    /// <typeparam name="TService">Service type that carries out the Crud ops</typeparam>
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
        public virtual void Create([FromBody] TModel model)
            => Service.Create(model);
        /// <summary>
        /// Delete op
        /// </summary>
        /// <param name="id">The id : 24 string to delete</param>
        /// <returns>True : success, false : else</returns>
        [HttpDelete("{id:length(24)}")]
        public virtual bool Delete(string id)
            => Service.Delete(id);
        /// <summary>
        /// get all op
        /// @TODO pagination or result limit
        /// </summary>
        /// <returns>ICollection<TModel> (List) eventually 0 sized</returns>
        [HttpGet]
        public virtual IEnumerable<TModel> GetAll()
            => Service.GetAll();
        /// <summary>
        /// get op
        /// </summary>
        /// <param name="id">The id : 24 string to Read</param>
        /// <returns>The json serialized object eventually default</returns>
        [HttpGet("{id:length(24)}")]
        public virtual TModel Read([FromRoute] string id)
            => Service.Read(id);
        /// <summary>
        /// update op
        /// </summary>
        /// <param name="id">Target resource id</param>
        /// <param name="model">the new Json serialized TModel type in Body</param>
        /// <returns>True : success, false : else</returns>
        [HttpPut("{id:length(24)}")]
        public virtual bool Update([FromRoute] string id, [FromBody] TModel model)
            => Service.Update(id, model);
    }
}
