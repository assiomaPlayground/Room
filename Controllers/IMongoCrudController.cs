using RoomService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Controllers
{
    public interface IMongoCrudController<TModel> 
        where TModel : IModel
    {
        ICollection<IModel> GetAll();
        void Create(TModel model);
        TModel Read(string id);
        bool Update(TModel model);
        bool Delete(string id);
    }
}
