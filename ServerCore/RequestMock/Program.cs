string url = "http://127.0.0.1:13000/api/items.create";
string filePath = @"E:\\MyProgs\\Home\\c sharp\\MongoServerTask\\ServerCore\\RequestMock\\item_create.json";

HttpClient HttpClient = new();

var fs = new FileStream(filePath, FileMode.Open);
string item = new StreamReader(fs).ReadToEnd();

HttpRequestMessage request = new(HttpMethod.Get, url)
{
    Content = new StringContent(item)
};

Console.WriteLine(request.Content.ReadAsStringAsync().Result);

try
{
    var response = HttpClient.Send(request);
    Console.WriteLine();
    Console.WriteLine(new StreamReader(response.Content.ReadAsStream()).ReadToEnd());
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}

Thread.Sleep(-1);