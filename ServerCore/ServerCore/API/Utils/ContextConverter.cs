namespace ServerCore.API.Utils
{
    public static class ContextConverter
    {
        public static Dictionary<string, string> GetKeyValuePairs(string url)
        {
            try
            {
                string parameters = url.Split("?").ElementAt(1);
                if (string.IsNullOrEmpty(parameters))
                {
                    throw new NullReferenceException("No parameters specified");
                }
                List<string> keyValuePairs = parameters.Split("&").ToList();
                var result = new Dictionary<string, string>();
                keyValuePairs.ForEach(keyValuePair => result.Add(keyValuePair.Split("=")[0], keyValuePair.Split("=")[1]));
                return result;
            }
            catch (ArgumentOutOfRangeException)
            {
                return new();
            }
        }
    }
}
