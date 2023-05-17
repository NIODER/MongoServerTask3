using ServerCore.API.Handlers;
using ServerCore.API.Utils;
using System.Net;

namespace ServerCore.API.Factories
{
    internal class EmployeeDeleteFactory : IHandlerFactory
    {
        public IHandler Create(HttpListenerContext context)
        {
            Logger.Log(LogSeverity.Info, nameof(EmployeeDeleteFactory), "Executed");
            ArgumentNullException.ThrowIfNull(context.Request.RawUrl);
            var parameters = ContextConverter.GetKeyValuePairs(context.Request.RawUrl);
            string? id = parameters.GetValueOrDefault("id");
            if (id == null)
            {
                return new EmployeeDelete(CoreException.InvalidIdException);
            }
            return new EmployeeDelete(id);
        }
    }
}
