using ServerCore.API.Handlers;
using ServerCore.API.Utils;
using System.Net;

namespace ServerCore.API.Factories
{
    internal class ItemsDeleteFactory : IHandlerFactory
    {
        public IHandler Create(HttpListenerContext context)
        {
            Logger.Log(LogSeverity.Info, nameof(ItemsDeleteFactory), "Executed");
            if (context.Request.RawUrl == null)
            {
                throw new NullReferenceException();
            }
            var parameters = ContextConverter.GetKeyValuePairs(context.Request.RawUrl);
            string? id = parameters.GetValueOrDefault("id");
            if (id == null)
            {
                return new ItemsDelete(CoreException.InvalidIdException);
            }
            return new ItemsDelete(id);
        }
    }
}
