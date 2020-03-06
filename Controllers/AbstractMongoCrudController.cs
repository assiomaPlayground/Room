using Microsoft.AspNetCore.Mvc;
using RoomService.Models;
using RoomService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Controllers
{
    public class AbstractMongoCrudController<TModel, TService> : ControllerBase, IMongoCrudController<TModel>
        where TModel : class, IModel
        where TService : AbstractMongoCrudService<TModel>
    {
        public AbstractMongoCrudController(TService service)
            => this.Service = service;
        protected TService Service { get; set; }
        [HttpPost]
        public virtual void Create([FromBody] TModel model)
        {
            Service.Create(model);
        }
        [HttpDelete("{id:length(24)}")]
        public virtual bool Delete(string id)
        {
            return Service.Delete(id);
        }
        [HttpGet]
        public virtual ICollection<IModel> GetAll()
        {
            var rawList = Service.GetAll();
            var result = new List<IModel>();
            foreach (var item in rawList)
                result.Add(item as TModel);
            return result;
        }
        [HttpGet("{id:length(24)}")]
        public TModel Read(string id)
        {
            return Service.Read(id);
        }
        [HttpPut]
        public bool Update([FromBody] TModel model)
        {
            return Service.Update(model);
        }
    }
}
