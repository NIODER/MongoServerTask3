using Database.Entities;
using Newtonsoft.Json;
using ServerCore.API.Handlers;
using System.Net;

namespace ServerCore.API.Factories
{
    internal class EmployeeCreateFactory : IHandlerFactory
    {
        public IHandler Create(HttpListenerContext context)
        {
            Logger.Log(LogSeverity.Info, nameof(EmployeeCreateFactory), "Executed");
            string json = new StreamReader(context.Request.InputStream).ReadToEnd();
            try
            {
                Employee? item = JsonConvert.DeserializeObject<Employee>(json);
                if (item == null)
                    return new EmployeeCreate(CoreException.EmptyBodyException);
                return new EmployeeCreate(item);
            }
            catch (JsonReaderException)
            {
                return new EmployeeCreate(CoreException.InvalidBodyException);
            }
        }
    }
}
