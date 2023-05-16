using Database;
using Database.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using ServerCore.API.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore.API.Handlers
{
    internal class ItemsDelete : IHandler
    {
        public string Id { get; set; } = string.Empty;
        public CoreException? CoreException { get; set; }

        public ItemsDelete(string id)
        {
            Id = id;
        }

        public ItemsDelete(CoreException coreException)
        {
            CoreException = coreException;
        }

        public Response ProcessRequest()
        {
            if (CoreException != null)
            {
                Logger.Log(LogSeverity.Info, nameof(ItemsDelete), $"Error {CoreException.Code}");
                return new()
                {
                    Exception = CoreException
                };
            }
            var database = new DatabaseDriver(Config.MongoConnectionString);
            Item? item = database.GetEntitiesPage(
                Config.MongoDatabaseName, 
                Config.ITEMS_COLLECTION_NAME, 
                Builders<Item>.Filter.Where(item => item.Id == Id))
                .FirstOrDefault();
            if (item == null)
            {
                Logger.Log(LogSeverity.Info, nameof(ItemsDelete), $"Success. Item is not found");
                return new()
                {
                    ResponseObjects = new()
                };
            }
            database.DeleteOneEntity(Config.MongoDatabaseName, Config.ITEMS_COLLECTION_NAME, item);
            Logger.Log(LogSeverity.Info, nameof(ItemsDelete), $"Success. Item deleted: {item.ToJson()}");
            return new()
            {
                ResponseObjects = new()
                {
                    item
                }
            };
        }
    }
}
