using Database.Entities;
using MongoDB.Bson;
using Npgsql;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;

namespace ServerCore.Model
{
    public class DatabaseInteractor
    {
        public NpgsqlDataSource DataSource { get; set; }

        public DatabaseInteractor()
        {
            DataSource = NpgsqlDataSource.Create(Config.PostgresConnectionString);
        }

        public long GetEmployeeCount()
        {
            var cmd = DataSource.CreateCommand($"SELECT COUNT(*) FROM employees;");
            var reader = cmd.ExecuteReader();
            if (reader.Read())
                return reader.GetInt64(0);
            throw new Exception("Error to read count of employee.");
        }

        public Employee GetEmployee(EmployeeFilterBuilder employeeFilterBuilder)
        {
            var cmd = DataSource.CreateCommand($"SELECT * FROM employees " +
                $"WHERE {employeeFilterBuilder.GetCondition()};");
            var reader = cmd.ExecuteReader();
            Employee employee = new();
            if (reader.Read())
            {
                employee.Id = reader.GetString(0);
                employee.Name = reader.GetString(1);
                employee.Position = reader.GetString(2);
                employee.Salary = reader.GetInt32(3);
                employee.PasswordData = reader.GetInt64(4);
                employee.Address = reader.GetString(5);
                employee.PhoneNumber = reader.GetString(6);
                employee.Email = reader.GetString(7);
            }
            return employee;
        }

        public List<Employee> GetEmployees(EmployeeFilterBuilder employeeFilterBuilder, int page = 0, int pageSize = 10)
        {
            var cmd = DataSource.CreateCommand($"SELECT * FROM employees " +
                $"WHERE {employeeFilterBuilder.GetCondition()} " +
                $"LIMIT {pageSize} OFFSET {page};");
            var reader = cmd.ExecuteReader();
            var employees = new List<Employee>();
            while (reader.Read())
            {
                var employee = new Employee()
                {
                    Id = reader.GetString(0),
                    Name = reader.GetString(1),
                    Position = reader.GetString(2),
                    Salary = reader.GetInt32(3),
                    PasswordData = reader.GetInt64(4),
                    Address = reader.GetString(5),
                    PhoneNumber = reader.GetString(5),
                    Email = reader.GetString(7)
                };
                employees.Add(employee);
            }
#warning убрать потом
            if (employees.Count > pageSize)
            {
                throw new Exception($"employees.Count ({employees.Count}) > pageSize ({pageSize})");
            }
            return employees;
        }

        public void CreateEmployee(EmployeeFilterBuilder employeeFilterBuilder)
        {
            var cmd = DataSource.CreateCommand($"INSERT INTO employees " +
                $"VALUES " +
                $"(\'{ObjectId.GenerateNewId()}\', {employeeFilterBuilder.GetParameters()});");
            cmd.ExecuteNonQuery();
        }

        public void DeleteEmployee(EmployeeFilterBuilder employeeFilterBuilder)
        {
            var cmd = DataSource.CreateCommand($"DELETE FROM employees " +
                $"WHERE {employeeFilterBuilder.GetCondition()};");
            cmd.ExecuteNonQuery();
        }
    }
}