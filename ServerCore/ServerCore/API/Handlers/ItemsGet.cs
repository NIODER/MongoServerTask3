using Database;
using Database.Entities;
using MongoDB.Driver;
using ServerCore.API.IO;

namespace ServerCore.API.Handlers
{
    internal class ItemsGet : IHandler
    {
        public string? id;
        public string? name;
        public DbEntityFilter.FilterEnum? price_filter;
        public long? price;
        public DbEntityFilter.FilterEnum? count_filter;
        public long? count;
        public int? page;
        public int? page_size;
        public CoreException? exception;

        public ItemsGet(RequestParameters parameters)
        {
            id = parameters.UrlParameters.GetValueOrDefault("id");
            name = parameters.UrlParameters.GetValueOrDefault("name");
            var priceFilter = parameters.UrlParameters.GetValueOrDefault("price_filter");
            var _price = parameters.UrlParameters.GetValueOrDefault("price");
            var countFilter = parameters.UrlParameters.GetValueOrDefault("count_filter");
            var _count = parameters.UrlParameters.GetValueOrDefault("count");
            var _page = parameters.UrlParameters.GetValueOrDefault("page");
            var pageSize = parameters.UrlParameters.GetValueOrDefault("page_size");
            if (priceFilter != null)
            {
                if (int.TryParse(priceFilter, out int f))
                {
                    price_filter = (DbEntityFilter.FilterEnum)f;
                }
                else
                {
                    exception = CoreException.InvalidDataTypeException;
                    exception.Message += $": price_filter";
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
                    exception.Message += $": price";
                }
            }
            if (countFilter != null)
            {
                if (int.TryParse(countFilter, out int f))
                {
                    count_filter = (DbEntityFilter.FilterEnum)f;
                }
                else
                {
                    exception = CoreException.InvalidDataTypeException;
                    exception.Message += $": count_filter";
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
                    exception.Message += $": count";
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
                    exception.Message += $": page";
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
                    exception.Message += $": page_size";
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

        public DbEntityFilter GetFilter()
        {
            var filter = new DbEntityFilter();
            if (name != null)
                filter.FilterParameters.Add(nameof(name), new(DbEntityFilter.FilterEnum.Equal, name));
            if (price != null)
                filter.FilterParameters.Add(nameof(price), new(price_filter ?? DbEntityFilter.FilterEnum.Equal, price));
            if (count != null)
                filter.FilterParameters.Add(nameof(count), new(price_filter ?? DbEntityFilter.FilterEnum.Equal, count));
            Logger.Log(LogSeverity.Debug, nameof(DbEntityFilter), filter.ToBsonDocument().ToString());
            return filter;
        }
    }
}
