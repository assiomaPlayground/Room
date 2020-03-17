using Microsoft.AspNetCore.Mvc;
using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Controllers
{
    /// <summary>
    /// Interface for generic CRUD ops controller
    /// </summary>
    /// <typeparam name="TModel">A target class model type</typeparam>
    public interface IMongoCrudController<TModel> 
        where TModel : class, IModel
    {
        ActionResult<IEnumerable<TModel>> GetAll();
        ActionResult<TModel> Create(TModel model);
        ActionResult<TModel> Read(string id);
        IActionResult Update(string id, TModel model);
        IActionResult Delete(string id);
    }
}
