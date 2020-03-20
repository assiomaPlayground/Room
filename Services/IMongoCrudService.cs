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
    /// Interface for Crud service
    /// </summary>
    /// <typeparam name="TModel">The target model type</typeparam>
    public interface IMongoCrudService<TModel>
        where TModel : IModel
    {
        /// <summary>
        /// Service init for base service generic initialization
        /// </summary>
        /// <param name="settings">The mongo settings wrapper</param>
        /// <param name="baseCollection">The base collection of the service</param>
        void Init(IMongoSettings settings, string baseCollection);
        /// <summary>
        /// Generic crud create implementation
        /// </summary>
        /// <param name="model">The data to create</param>
        /// <returns>The created data and is location (Id) in the database</returns>
        TModel Create(TModel model);
        /// <summary>
        /// Generic crud create implementation
        /// </summary>
        /// <param name="id">The target id To Delete</param>
        /// <returns>Delete result</returns>
        DeleteResult Delete(string id);
        /// <summary>
        /// Generic crud Read implementation
        /// </summary>
        /// <param name="id">The resource to read</param>
        /// <returns>The resource found in database</returns>
        TModel Read(string id);
        /// <summary>
        /// Generic crud Update implementation
        /// </summary>
        /// <param name="id">The resource id</param>
        /// <param name="newModel">The new data</param>
        /// <returns></returns>
        ReplaceOneResult Update(string id, TModel newModel);
        /// <summary>
        /// Read all
        /// </summary>
        /// <returns>All data in the path</returns>
        IEnumerable<TModel> GetAll();
    }
}
