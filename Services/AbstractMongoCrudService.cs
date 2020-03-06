using MongoDB.Driver;
using RoomService.Models;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Services
{
    public abstract class AbstractMongoCrudService<TModel> : IMongoCrudService<TModel>
        where TModel : IModel
    {
        protected IMongoCollection<TModel> Collection { get; private set; }
        public virtual void Init(IMongoSettings settings, string baseCollection)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Collection = database.GetCollection<TModel>(baseCollection);
        }
        public virtual void Create(TModel model) => Collection.InsertOne(model);
        public virtual bool Delete(string id) => Collection.DeleteOne(model => model.Id == id).IsAcknowledged;
        public virtual TModel Read(string id) => Collection.Find<TModel>(model => model.Id == id).FirstOrDefault<TModel>();
        public virtual bool Update(TModel newModel) => Collection.ReplaceOne<TModel>(model => newModel.Id == model.Id, newModel).IsAcknowledged;
        public virtual ICollection<TModel> GetAll() => Collection.Find<TModel>(model => true).ToList<TModel>();
    }
}
