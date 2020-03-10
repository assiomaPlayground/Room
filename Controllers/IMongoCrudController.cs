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
        IEnumerable<TModel> GetAll();
        void Create(TModel model);
        TModel Read(string id);
        bool Update(string id, TModel model);
        bool Delete(string id);
    }
}
