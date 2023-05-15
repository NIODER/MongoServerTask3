using Database.Entities;
using Newtonsoft.Json;
using ServerCore.API.Handlers;
using System.Net;

namespace ServerCore.API.Factories
{
    internal class ItemsCreateFactory : IHandlerFactory
    {
        public IHandler Create(HttpListenerContext context)
        {
            Logger.Log(LogSeverity.Info, nameof(ItemsCreateFactory), "Executed");
            string json = new StreamReader(context.Request.InputStream).ReadToEnd();
            try
            {
                Item? item = JsonConvert.DeserializeObject<Item>(json);
                if (item == null)
                    return new ItemsCreate(CoreException.EmptyBodyException);
                return new ItemsCreate(item);
            }
            catch (JsonReaderException)
            {
                return new ItemsCreate(CoreException.InvalidBodyException);
            }
        }
    }
}
