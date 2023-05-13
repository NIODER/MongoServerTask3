using System.Text.Json.Serialization;

namespace ServerCore.API.IO
{
    internal class Response
    {
        [JsonPropertyName("error"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CoreException? Exception { get; set; }
        [JsonPropertyName("response"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<object>? ResponseObjects { get; set; }
    }
}
