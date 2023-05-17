using Database.Entities;
using ServerCore.Model;

namespace ServerCoreTests
{
    public class UnitTest1
    {
        [Fact]
        public void EmployeeCreateTest()
        {
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
        }
    }
}