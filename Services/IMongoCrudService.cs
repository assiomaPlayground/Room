using RoomService.Models;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Services
{
    public interface IMongoCrudService<TModel>
        where TModel : IModel
    {
        void Init(IMongoSettings settings, string baseCollection);
        void Create(TModel model);
        bool Delete(string id);
        TModel Read(string id);
        bool Update(TModel newModel);
        ICollection<TModel> GetAll();
    }
}
