using ServerCore.API.Handlers;
using ServerCore.API.IO;
using ServerCore.API.Utils;
using System.Net;

namespace ServerCore.API.Factories
{
    internal class ItemsCountFactory : IHandlerFactory
    {
        public IHandler Create(HttpListenerContext context)
        {
            Logger.Log(LogSeverity.Info, nameof(ItemsCountFactory), "Executed");
            if (context.Request.Url == null)
            {
                throw new NullReferenceException("Uri was null");
            }
            var urlParameters = ContextConverter.GetKeyValuePairs(context.Request.Url.ToString());
            return new ItemsCount(new RequestParameters()
            {
                UrlParameters = urlParameters
            });
        }
    }
}
