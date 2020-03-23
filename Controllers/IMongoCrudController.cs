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
        /// <returns>Action Result</returns>
        ActionResult<IEnumerable<TModel>> GetAll();

        /// <summary>
        /// Result Action Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Action Result(Create)</returns>
        ActionResult<TModel> Create(TModel model);

        /// <summary>
        /// Result Action Read
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Action Result(Read)</returns>
        ActionResult<TModel> Read(string id);

        /// <summary>
        /// Result ActionUpdate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Update(id,model)</returns>
        IActionResult Update(string id, TModel model);

        /// <summary>
        /// Result Action Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete(id)</returns>
        IActionResult Delete(string id);
    }
}
