﻿using Database;
using Database.Entities;
using ServerCore.API.IO;
using System.Text.Json.Serialization;

namespace ServerCore.API.Handlers
{
    internal class ItemsCount : IHandler
    {
        class CountResult
        {
            [JsonPropertyName("count")]
            public long Count { get; set; }
        }

        private readonly string? item_id;
        private readonly DatabaseDriver database;

        public ItemsCount(RequestParameters parameters)
        {
            item_id = parameters.UrlParameters.GetValueOrDefault("item_id");
            database = new(Config.MongoConnectionString);
        }

        public Response ProcessRequest()
        {
            long count;
            try
            {
                count = item_id == null ?
                    GetAllItemsCount() :
                    GetItemsCount();
            }
            catch (ArgumentNullException)
            {
                Logger.Log(LogSeverity.Info, nameof(ItemsCount), "Item not found");
                return new Response()
                {
                    ResponseObjects = new()
                };
            }
            catch (Exception e)
            {
                Logger.Log(LogSeverity.Debug, nameof(ItemsCount), "Exception occured", e);
                return new Response()
                {
                    Exception = CoreException.UnknownException
                };
            }
            return new Response()
            {
                ResponseObjects = new()
                {
                    new CountResult { Count = count }
                }
            };
        }

        private long GetAllItemsCount()
        {
            return database.GetCollectionEntitiesCount(Config.MongoDatabaseName, Config.ITEMS_COLLECTION_NAME);
        }

        private long GetItemsCount()
        {
            return database.GetEntitiesPage<Item>(
                Config.MongoDatabaseName, 
                Config.ITEMS_COLLECTION_NAME, 
                item => item.Id.ToString() == item_id)
                .First()
                .Count;
        }
    }
}