using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ServerCore.API.IO;

namespace ServerCore.API
{
    internal interface IHandlerFactory
    {
        IHandler Create(HttpListenerContext context);

    }
}
