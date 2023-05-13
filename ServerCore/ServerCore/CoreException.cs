using System.Text.Json.Serialization;

namespace ServerCore
{
    internal class CoreException
    {
        [JsonPropertyName("error-code")]
        public int Code { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }

        public CoreException(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public static CoreException InvalidIdException { get => new(1, Config.Exception1Message); }
        public static CoreException PageNumberOutOfRangeException { get => new(2, Config.Exception2Message); }
        public static CoreException PageSizeNullException { get => new(3, Config.Exception3Message); }
        public static CoreException InvalidDataTypeException { get => new(4, Config.Exception4Message); }
        public static CoreException UnknownException { get => new(5, Config.Exception5Message); }
        public static CoreException AccessException { get => new(6, Config.Exception6Message); }
    }
}
