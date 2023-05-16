using Database.Entities;
using MongoDB.Bson;
using Npgsql;
using System.Collections.Generic;

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