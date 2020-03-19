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
    /// <typeparam name="TModel">A Target class model type</typeparam>
    public interface IMongoCrudController<TModel> 
        where TModel : class, IModel
    {

        /// <summary>
        /// Result Action Get All
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<TModel>> GetAll();

        /// <summary>
        /// Result Action Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ActionResult<TModel> Create(TModel model);

        /// <summary>
        /// Result Action Read
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<TModel> Read(string id);

        /// <summary>
        /// Result ActionUpdate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        IActionResult Update(string id, TModel model);

        /// <summary>
        /// Result Action Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IActionResult Delete(string id);
    }
}
