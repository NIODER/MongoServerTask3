using ServerCore.API.Handlers;
using ServerCore.API.IO;
using ServerCore.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore.API.Factories
{
    internal class EmployeeGetFactory : IHandlerFactory
    {
        public IHandler Create(HttpListenerContext context)
        {
            Logger.Log(LogSeverity.Info, nameof(EmployeeGetFactory), "Executed");

            ArgumentNullException.ThrowIfNull(context.Request.RawUrl);

            return new EmployeeGet(new RequestParameters()
            {
                UrlParameters = ContextConverter.GetKeyValuePairs(context.Request.RawUrl),
                Body = new StreamReader(context.Request.InputStream).ReadToEnd()
            });
        }
    }
}
