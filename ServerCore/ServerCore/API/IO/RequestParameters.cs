using ServerCore.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore.API.IO
{
    internal class RequestParameters
    {
        public Dictionary<string, string> UrlParameters { get; set; } = new();
        public object? Body { get; set; }
    }
}
