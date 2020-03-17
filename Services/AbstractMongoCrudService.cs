using MongoDB.Driver;
using RoomService.Models;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Services
{
    /// <summary>
    /// Abstract crud service use driver for ops in mongo repo
    /// </summary>
    /// <typeparam name="TModel">The context model type</typeparam>
    public abstract class AbstractMongoCrudService<TModel> : IMongoCrudService<TModel>
        where TModel : class, IModel
    {
        /// <summary>
        /// Database Connection
        /// </summary>
        protected IMongoDatabase Database { get; private set; }
        /// <summary>
        /// The mongo repository
        /// </summary>
        public IMongoCollection<TModel> Collection { get; private set; }
        /// <summary>
        /// Init the mongo client and database
        /// @TODO: build specific settings class instead of passing everything
        /// </summary>
        /// <param name="settings">The mongo settings file</param>
        /// <param name="baseCollection">The service collection context</param>
        public virtual void Init(IMongoSettings settings, string baseCollection)
        {
            var client = new MongoClient(settings.ConnectionString);
            this.Database = client.GetDatabase(settings.DatabaseName);

            Collection = Database.GetCollection<TModel>(baseCollection);
        }
        /// <summary>
        /// Create op
        /// </summary>
        /// <param name="model">TModel type class</param>
        public virtual TModel Create(TModel model)
        {
            Collection.InsertOne(model);
            return model;
        }
        /// <summary>
        /// Delete op
        /// </summary>
        /// <param name="id">The id : 24 string to delete</param>
        /// <returns>True : success, false : else</returns>
        public virtual DeleteResult Delete(string id) 
            => Collection.DeleteOne(model => model.Id == id);
        /// <summary>
        /// get op
        /// </summary>
        /// <param name="id">The id : 24 string to Read</param>
        /// <returns>the object maching the id, eventually default</returns>
        public virtual TModel Read(string id) 
            =>  Collection.Find<TModel>(model => model.Id == id).FirstOrDefault<TModel>();
        /// <summary>
        /// update op
        /// </summary>
        /// <param name="id">Target resource id</param>
        /// <param name="model">TModel type object</param>
        /// <returns>True : success, false : else</returns>
        public virtual ReplaceOneResult Update(string id, TModel newModel) 
            => Collection.ReplaceOne<TModel>(model => id == model.Id, newModel);
        /// <summary>
        /// get all op
        /// @TODO pagination or result limit
        /// </summary>
        /// <returns>ICollection<TModel> (List) eventually 0 sized</returns>
        public virtual IEnumerable<TModel> GetAll() 
            => Collection.Find<TModel>(model => true).ToEnumerable<TModel>();
    }
}
