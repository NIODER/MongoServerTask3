using ServerCore.API;
using ServerCore.API.Factories;
using ServerCore.API.IO;
using System.Net;
using System.Text.Json;

namespace ServerCore
{
    internal class HttpHandler
    {
        public const string ITEMS_COUNT_METHOD = "items.count";
        public const string ITEMS_GET_METHOD = "items.get";
        public const string ITEMS_CREATE_METHOD = "items.create";
        public const string ITEMS_DELETE_METHOD = "items.delete";
        public const string EMPLOYEE_COUNT_METHOD = "employee.count";
        public const string EMPLOYEE_GET_METHOD = "employee.get";
        public const string EMPLOYEE_CREATE_METHOD = "employee.create";
        public const string EMPLOYEE_DELETE_METHOD = "employee.delete";

        private string ExtractMethod(string url)
        {
            return url.Split("?")[0].Split("/")[^1];
        }

        private IHandlerFactory GetHandlerFactory(string method)
        {
            return method switch
            {
                ITEMS_COUNT_METHOD => new ItemsCountFactory(),
                ITEMS_GET_METHOD => new ItemsGetFactory(),
                ITEMS_CREATE_METHOD => new ItemsCreateFactory(),
                ITEMS_DELETE_METHOD => new ItemsDeleteFactory(),
                EMPLOYEE_COUNT_METHOD => new EmployeeCountFactory(),
                EMPLOYEE_GET_METHOD => new EmployeeGetFactory(),
                EMPLOYEE_CREATE_METHOD => new EmployeeCreateFactory(),
                EMPLOYEE_DELETE_METHOD => new EmployeeDeleteFactory(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void ExecuteHandler(HttpListenerContext context)
        {
            string method = ExtractMethod(context.Request.RawUrl ?? string.Empty);
            IHandlerFactory handlerFactory;
            try
            {
                handlerFactory = GetHandlerFactory(method);
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
            Response response = new() { Exception = CoreException.UnknownException };
            try
            {
                IHandler handler = handlerFactory.Create(context);
                response = handler.ProcessRequest();
            }
            catch (Exception e)
            {
                Logger.Log(LogSeverity.Error, nameof(HttpHandler), "Error", e);
            }
            JsonSerializer.Serialize(context.Response.OutputStream, response);
            context.Response.OutputStream.Close();
        }
    }
}
