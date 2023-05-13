using ServerCore.API.Handlers;
using ServerCore.API.IO;
using ServerCore.API.Utils;
using System.Net;

namespace ServerCore.API.Factories
{
    internal class ItemsGetFactory : IHandlerFactory
    {
        public IHandler Create(HttpListenerContext context)
        {
            if (context.Request.RawUrl == null)
            {
                throw new NullReferenceException();
            }

            return new ItemsGet(new RequestParameters()
            {
                UrlParameters = ContextConverter.GetKeyValuePairs(context.Request.RawUrl),
                Body = new StreamReader(context.Request.InputStream).ReadToEnd()
            });
        }
    }
}
