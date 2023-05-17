using ServerCore.API.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore.API.Factories
{
    internal class EmployeeCountFactory : IHandlerFactory
    {
        public IHandler Create(HttpListenerContext context) => new EmployeeCount();
    }
}
