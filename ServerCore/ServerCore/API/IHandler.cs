using ServerCore.API.IO;

namespace ServerCore.API
{
    internal interface IHandler
    {
        Response ProcessRequest();
    }
}
