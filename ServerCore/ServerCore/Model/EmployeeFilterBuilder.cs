using Amazon.Auth.AccessControlPolicy;
using Database.Entities;
using MongoDB.Driver.Core.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore.Model
{
    public class EmployeeFilterBuilder
    {
        public string? Id { get; private set; }
        public string? Name { get; private set; }
        public string? Position { get; private set; }
        public int? Salary { get; private set; }
        public long? PasswordData { get; private set; }
        public string? Address { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? Email { get; private set; }

        public EmployeeFilterBuilder WithId(string id)
        {
            Id = id;
            return this;
        }

        public EmployeeFilterBuilder WithName(string name) 
        { 
            Name = name; 
            return this; 
        }

        public EmployeeFilterBuilder WithPosition(string position)
        {
            Position = position;
            return this;
        }

        public EmployeeFilterBuilder WithSalary(int salary)
        {
            Salary = salary;
            return this;
        }

        public EmployeeFilterBuilder WithPasswordData(long passwordData)
        {
            PasswordData = passwordData;
            return this;
        }

        public EmployeeFilterBuilder WithAddress(string address)
        {
            Address = address;
            return this;
        }

        public EmployeeFilterBuilder WithPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            return this;
        }

        public EmployeeFilterBuilder WithEmail(string email)
        {
            Email = email;
            return this;
        }

        private List<(string name, string value)> GetConditions()
        {
            List<(string name, string value)> conditions = new();
            conditions.Add((DbEntity.ID_PROPERTY, Id ?? string.Empty));
            if (!string.IsNullOrEmpty(Name))
            {
                conditions.Add((Employee.NAME_PROPERTY, Name));
            }
            if (!string.IsNullOrEmpty(Position))
            {
                conditions.Add((Employee.POSITION_PROPERTY, Position));
            }
            if (!string.IsNullOrEmpty(Salary?.ToString()))
            {
                conditions.Add((Employee.SALARY_PROPERTY, Salary.Value.ToString()));
            }
            if (!string.IsNullOrEmpty(PasswordData?.ToString()))
            {
                conditions.Add((Employee.PASSWORD_DATA_PROPERTY, PasswordData.Value.ToString()));
            }
            if (!string.IsNullOrEmpty(Address))
            {
                conditions.Add((Employee.RESIDENTIAL_ADDRESS_PROPERTY, Address));
            }
            if (!string.IsNullOrEmpty(PhoneNumber))
            {
                conditions.Add((Employee.PHONE_PROPERTY, PhoneNumber));
            }
            if (!string.IsNullOrEmpty(Email))
            {
                conditions.Add((Employee.EMAIL_PROPERTY, Email));
            }
            return conditions;
        }

        public string GetCondition()
        {
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(Id))
            {
                condition += $"{DbEntity.ID_PROPERTY} = \'{Id}\' AND ";
            }
            if (!string.IsNullOrEmpty(Name))
            {
                condition += $"{Employee.NAME_PROPERTY} = \'{Name}\' AND ";
            }
            if (!string.IsNullOrEmpty(Position))
            {
                condition += $"{Employee.POSITION_PROPERTY} = \'{Position}\' AND ";
            }
            if (!string.IsNullOrEmpty(Salary?.ToString()))
            {
                condition += $"{Employee.SALARY_PROPERTY} = {Salary.Value} AND ";
            }
            if (!string.IsNullOrEmpty(PasswordData?.ToString()))
            {
                condition += $"{Employee.PASSWORD_DATA_PROPERTY} = {PasswordData.Value} AND ";
            }
            if (!string.IsNullOrEmpty(Address))
            {
                condition += $"{Employee.RESIDENTIAL_ADDRESS_PROPERTY} = \'{Address}\' AND ";
            }
            if (!string.IsNullOrEmpty(PhoneNumber))
            {
                condition += $"{Employee.PHONE_PROPERTY} = \'{PhoneNumber}\' AND ";
            }
            if (!string.IsNullOrEmpty(Email))
            {
                condition += $"{Employee.EMAIL_PROPERTY} = \'{Email}\' AND ";
            }
            return condition.Remove(condition.Length - 5);
        }

        public string GetParameterNames()
        {
            return GetConditions().Select(p => p.name).Aggregate((p1, p2) => p1 + ", " + p2);
        }

        public string GetParameters()
        {
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(Id))
            {
                condition += $"\'{Id}\', ";
            }
            if (!string.IsNullOrEmpty(Name))
            {
                condition += $"\'{Name}\', ";
            }
            if (!string.IsNullOrEmpty(Position))
            {
                condition += $"\'{Position}\', ";
            }
            if (!string.IsNullOrEmpty(Salary?.ToString()))
            {
                condition += $"{Salary.Value}, ";
            }
            if (!string.IsNullOrEmpty(PasswordData?.ToString()))
            {
                condition += $"{PasswordData.Value}, ";
            }
            if (!string.IsNullOrEmpty(Address))
            {
                condition += $"\'{Address}\', ";
            }
            if (!string.IsNullOrEmpty(PhoneNumber))
            {
                condition += $"\'{PhoneNumber}\', ";
            }
            if (!string.IsNullOrEmpty(Email))
            {
                condition += $"\'{Email}\', ";
            }
            return condition.Remove(condition.Length - 2);
        }
    }
}
