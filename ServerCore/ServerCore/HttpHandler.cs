using ServerCore.API;
using ServerCore.API.Factories;
using ServerCore.API.IO;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
                ITEMS_CREATE_METHOD => throw new NotImplementedException(),
                ITEMS_DELETE_METHOD => throw new NotImplementedException(),
                EMPLOYEE_COUNT_METHOD => throw new NotImplementedException(),
                EMPLOYEE_GET_METHOD => throw new NotImplementedException(),
                EMPLOYEE_CREATE_METHOD => throw new NotImplementedException(),
                EMPLOYEE_DELETE_METHOD => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
        }

        public void ExecuteHandler(HttpListenerContext context)
        {
            string method = ExtractMethod(context.Request.RawUrl ?? string.Empty);
            IHandlerFactory handlerFactory = GetHandlerFactory(method);
            IHandler handler = handlerFactory.Create(context);
            Response response = handler.ProcessRequest();
            JsonSerializer.Serialize(context.Response.OutputStream, response);
            context.Response.OutputStream.Close();
        }
    }
}
