using Database;
using Database.Entities;
using MongoDB.Driver;
using ServerCore.API.IO;
using System.Text.Json.Serialization;

namespace ServerCore.API.Handlers
{
    internal class ItemsCreate : IHandler
    {
        public Item Item { get; set; }
        public CoreException? CoreException { get; set; }

        public ItemsCreate(Item item)
        {
            Item = item;
        }

        public ItemsCreate(CoreException coreException)
        {
            Item = new();
            CoreException = coreException;
        }

        public Response ProcessRequest()
        {
            if (CoreException != null)
            {
                Logger.Log(LogSeverity.Info, nameof(ItemsCreate), $"Error {CoreException.Code}");
                return new Response()
                {
                    Exception = CoreException
                };
            }
            var database = new DatabaseDriver(Config.MongoConnectionString);
            database.InsertOneEntity(Config.MongoDatabaseName, Config.ITEMS_COLLECTION_NAME, Item);
            Item = database.GetEntitiesPage(Config.MongoDatabaseName, Config.ITEMS_COLLECTION_NAME, GetFilter()).First();
            Logger.Log(LogSeverity.Info, nameof(ItemsCreate), $"Success");
            return new Response()
            {
                ResponseObjects = new() { Item }
            };
        }

        private FilterDefinition<Item> GetFilter()
        {
            return Builders<Item>.Filter.Where(item =>
                item.Name == Item.Name &&
                item.Price == Item.Price &&
                item.Company == Item.Company &&
                item.Count == Item.Count);
        }
    }
}
