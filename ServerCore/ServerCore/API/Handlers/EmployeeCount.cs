using ServerCore.API.IO;
using ServerCore.Model;
using System.Text.Json.Serialization;

namespace ServerCore.API.Handlers
{
    internal class EmployeeCount : IHandler
    {
        class CountResult
        {
            [JsonPropertyName("count")]
            public long Count { get; set; }
        }

        public Response ProcessRequest()
        {
            var database = new DatabaseInteractor();
            long count = database.GetEmployeeCount();
            return new()
            {
                ResponseObjects = new()
                {
                    new CountResult() { Count = count }
                }
            };
        }
    }
}
