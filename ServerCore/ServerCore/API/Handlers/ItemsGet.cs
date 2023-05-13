using Database;
using Database.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ServerCore.API.IO;

namespace ServerCore.API.Handlers
{
    internal class ItemsGet : IHandler
    {
        public string? id;
        public string? name;
        public FilterEnum? price_filter;
        public long? price;
        public FilterEnum? count_filter;
        public long? count;
        public int? page;
        public int? page_size;
        public CoreException? exception;

        public ItemsGet(RequestParameters parameters)
        {
            id = parameters.UrlParameters.GetValueOrDefault(DbEntity.ID_PROPERTY);
            name = parameters.UrlParameters.GetValueOrDefault(Item.NAME_PROPERTY);
            var priceFilter = parameters.UrlParameters.GetValueOrDefault(Config.PriceFilterProperty);
            var _price = parameters.UrlParameters.GetValueOrDefault(Item.PRICE_PROPERTY);
            var countFilter = parameters.UrlParameters.GetValueOrDefault(Config.CountFilterProperty);
            var _count = parameters.UrlParameters.GetValueOrDefault(Item.COUNT_PROPERTY);
            var _page = parameters.UrlParameters.GetValueOrDefault(Config.PageNumberProperty);
            var pageSize = parameters.UrlParameters.GetValueOrDefault(Config.PageSizeProperty);
            if (priceFilter != null)
            {
                if (int.TryParse(priceFilter, out int f))
                {
                    price_filter = (FilterEnum)f;
                }
                else
                {
                    exception = CoreException.InvalidDataTypeException;
                    exception.Message += $": {Config.PriceFilterProperty}";
                }
            }
            if (_price != null)
            {
                if (long.TryParse(_price, out long p))
                {
                    price = p;
                }
                else
                {
                    exception = CoreException.InvalidDataTypeException;
                    exception.Message += $": {Item.PRICE_PROPERTY}";
                }
            }
            if (countFilter != null)
            {
                if (int.TryParse(countFilter, out int f))
                {
                    count_filter = (FilterEnum)f;
                }
                else
                {
                    exception = CoreException.InvalidDataTypeException;
                    exception.Message += $": {Config.CountFilterProperty}";
                }
            }
            if (_count != null)
            {
                if (long.TryParse(_count, out long p))
                {
                    count = p;
                }
                else
                {
                    exception = CoreException.InvalidDataTypeException;
                    exception.Message += $": {Item.COUNT_PROPERTY}";
                }
            }
            if (_page != null)
            {
                if (int.TryParse(_page, out int pa))
                {
                    page = pa;
                }
                else
                {
                    exception = CoreException.InvalidDataTypeException;
                    exception.Message += $": {Config.PageNumberProperty}";
                }
            }
            if (page_size != null)
            {
                if (int.TryParse(pageSize, out int ps))
                {
                    page_size = ps;
                }
                else
                {
                    exception = CoreException.InvalidDataTypeException;
                    exception.Message += $": {Config.PageSizeProperty}";
                }
            }
        }
        
        public Response ProcessRequest()
        {
            if (exception != null)
            {
                return new Response()
                {
                    Exception = exception
                };
            }
            var database = new DatabaseDriver(Config.MongoConnectionString);
            if (id != null)
            {
                return new Response()
                {
                    ResponseObjects = database.GetEntitiesPage<Item>(
                            Config.MongoDatabaseName,
                            Config.ITEMS_COLLECTION_NAME,
                            item => item.Id.ToString() == id).OfType<object>().ToList()
                };
            }
            if (page == null)
            {
                return new Response()
                {
                    ResponseObjects = database.GetEntitiesPage<Item>(
                        Config.MongoDatabaseName,
                        Config.ITEMS_COLLECTION_NAME,
                        GetFilter()).OfType<object>().ToList()
                };
            }
            else
            {
                if (page_size == null)
                {
                    return new Response()
                    {
                        Exception = CoreException.PageSizeNullException
                    };
                }
                return new Response()
                {
                    ResponseObjects = database.GetEntitiesPage<Item>(
                        Config.MongoDatabaseName,
                        Config.ITEMS_COLLECTION_NAME,
                        GetFilter(), page.Value, page_size.Value).OfType<object>().ToList()
                };
            }
        }

        public FilterDefinition<Item> GetFilter()
        {
            var filterDefinitions = new List<FilterDefinition<Item>>();
            if (name != null)
                filterDefinitions.Add(Builders<Item>.Filter.Eq(DbEntity.ID_PROPERTY, name));
            if (price != null)
            {
                filterDefinitions.Add(price_filter switch
                {
                    FilterEnum.Equal => Builders<Item>.Filter.Eq(Item.PRICE_PROPERTY, price),
                    FilterEnum.NotEqual => Builders<Item>.Filter.Ne(Item.PRICE_PROPERTY, price),
                    FilterEnum.Greater => Builders<Item>.Filter.Gt(Item.PRICE_PROPERTY, price),
                    FilterEnum.GreaterOrEqual => Builders<Item>.Filter.Gte(Item.PRICE_PROPERTY, price),
                    FilterEnum.Less => Builders<Item>.Filter.Lt(Item.PRICE_PROPERTY, price),
                    FilterEnum.LessOrEqual => Builders<Item>.Filter.Lte(Item.PRICE_PROPERTY, price),
                    _ => Builders<Item>.Filter.Eq(Item.PRICE_PROPERTY, price)
                });
            }
            if (count != null)
            {
                filterDefinitions.Add(count_filter switch
                {
                    FilterEnum.Equal => Builders<Item>.Filter.Eq(Item.COUNT_PROPERTY, price),
                    FilterEnum.NotEqual => Builders<Item>.Filter.Ne(Item.COUNT_PROPERTY, price),
                    FilterEnum.Greater => Builders<Item>.Filter.Gt(Item.COUNT_PROPERTY, price),
                    FilterEnum.GreaterOrEqual => Builders<Item>.Filter.Gte(Item.COUNT_PROPERTY, price),
                    FilterEnum.Less => Builders<Item>.Filter.Lt(Item.COUNT_PROPERTY, price),
                    FilterEnum.LessOrEqual => Builders<Item>.Filter.Lte(Item.COUNT_PROPERTY, price),
                    _ => Builders<Item>.Filter.Eq(Item.COUNT_PROPERTY, price)
                });
            }
            if (filterDefinitions.Count == 0)
                return new BsonDocument();
            return filterDefinitions.First();
        }
    }
}
