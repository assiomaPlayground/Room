using RoomService.Models;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Services
{
    /// <summary>
    /// Interface for Crud service
    /// </summary>
    /// <typeparam name="TModel">The target model type</typeparam>
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
