//string url = "http://127.0.0.1:13000/api/employee.create";
//string filePath = @"E:\\MyProgs\\Home\\c sharp\\MongoServerTask\\ServerCore\\RequestMock\\item_create.json";

//HttpClient HttpClient = new();

//var fs = new FileStream(filePath, FileMode.Open);
//string item = new StreamReader(fs).ReadToEnd();

//HttpRequestMessage request = new(HttpMethod.Get, url)
//{
//    Content = new StringContent(item)
//};

//Console.WriteLine(request.Content.ReadAsStringAsync().Result);

//try
//{
//    var response = HttpClient.Send(request);
//    Console.WriteLine();
//    Console.WriteLine(new StreamReader(response.Content.ReadAsStream()).ReadToEnd());
//}
//catch (Exception e)
//{
//    Console.WriteLine(e.ToString());
//}

//Thread.Sleep(-1);


//using Amazon.Auth.AccessControlPolicy;
//using MongoDB.Bson;
//using ServerCore.Model;

//DatabaseInteractor databaseInteractor = new();
//EmployeeFilterBuilder employeeFilterBuilder = new EmployeeFilterBuilder().WithName("postgresNAme")
//    .WithSalary(123)
//    .WithPosition("otec")
//    .WithAddress("sadfasdf")
//    .WithEmail("asfasdfasfd")
//    .WithPasswordData(123123)
//    .WithPhoneNumber("8901101");

//databaseInteractor.CreateEmployee(employeeFilterBuilder);

//Console.WriteLine(databaseInteractor.GetEmployeeCount());

//var emp = databaseInteractor.GetEmployee(employeeFilterBuilder);

//Console.WriteLine(emp.ToJson());

//databaseInteractor.DeleteEmployee(new EmployeeFilterBuilder().WithId(emp.Id));


using Database.Entities;
using MongoDB.Bson;
using ServerCore.Model;

string url = "http://127.0.0.1:13000/api/employee.create";

var databaseInteractor = new DatabaseInteractor();
Employee employee = new()
{
    Name = "new+name",
    Position = "director of yandex",
    Salary = 1,
    PasswordData = 123123,
    Address = "13salkdfj",
    PhoneNumber = "8901912132",
    Email = "yandex.mail"
};

HttpClient HttpClient = new HttpClient();

HttpRequestMessage request = new(HttpMethod.Get, url)
{
    Content = new StringContent(employee.ToJson())
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