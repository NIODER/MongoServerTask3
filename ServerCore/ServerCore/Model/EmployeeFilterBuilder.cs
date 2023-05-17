using Amazon.Auth.AccessControlPolicy;
using Database.Entities;
using MongoDB.Bson;
using MongoDB.Driver.Core.Connections;
using ServerCore.API.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServerCore.Model
{
    public class EmployeeFilterBuilder
    {
        public string? Id { get; private set; }
        public string? Name { get; private set; }
        public string? Position { get; private set; }
        public int? Salary { get; private set; }
        public FilterEnum? FilterEnum { get; private set; }
        public long? PasswordData { get; private set; }
        public string? Address { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? Email { get; private set; }

        public EmployeeFilterBuilder()
        {
        }

        public EmployeeFilterBuilder(Employee employee)
        {
            Id = employee.Id;
            Name = employee.Name;
            Position = employee.Position;
            Salary = employee.Salary;
            PasswordData = employee.PasswordData;
            Address = employee.Address;
            PhoneNumber = employee.PhoneNumber;
            Email = employee.Email;
        }

        public EmployeeFilterBuilder WithId(string id)
        {
            if (CheckForSqlInjection(id))
            {
                throw new ArgumentException("parameter has invalid characters", nameof(id));
            }
            Id = id;
            return this;
        }

        public EmployeeFilterBuilder WithName(string name) 
        { 
            if (CheckForSqlInjection(name))
            {
                throw new ArgumentException("parameter has invalid characters", nameof(name));
            }
            Name = name;
            return this; 
        }

        public EmployeeFilterBuilder WithPosition(string position)
        {
            if (CheckForSqlInjection(position))
            {
                throw new ArgumentException("parameter has invalid characters", nameof(position));
            }
            Position = position;
            return this;
        }

        public EmployeeFilterBuilder WithSalary(int salary)
        {
            Salary = salary;
            FilterEnum = API.IO.FilterEnum.Equal;
            return this;
        }

        public EmployeeFilterBuilder WithSalary(int salary, FilterEnum filter)
        {
            Salary = salary;
            FilterEnum = filter;
            return this;
        }

        public EmployeeFilterBuilder WithPasswordData(long passwordData)
        {
            PasswordData = passwordData;
            return this;
        }

        public EmployeeFilterBuilder WithAddress(string address)
        {
            if (CheckForSqlInjection(address))
            {
                throw new ArgumentException("parameter has invalid characters", nameof(address));
            }
            Address = address;
            return this;
        }

        public EmployeeFilterBuilder WithPhoneNumber(string phoneNumber)
        {
            if (CheckForSqlInjection(phoneNumber))
            {
                throw new ArgumentException("parameter has invalid characters", nameof(phoneNumber));
            }
            PhoneNumber = phoneNumber;
            return this;
        }

        public EmployeeFilterBuilder WithEmail(string email)
        {
            if (CheckForSqlInjection(email))
            {
                throw new ArgumentException("parameter has invalid characters", nameof(email));
            }
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
                if (FilterEnum.HasValue)
                {
                    switch (FilterEnum.Value)
                    {
                        case API.IO.FilterEnum.Equal:
                            condition += $"{Employee.SALARY_PROPERTY} = {Salary.Value} AND ";
                            break;
                        case API.IO.FilterEnum.NotEqual:
                            condition += $"{Employee.SALARY_PROPERTY} != {Salary.Value} AND ";
                            break;
                        case API.IO.FilterEnum.Greater:
                            condition += $"{Employee.SALARY_PROPERTY} > {Salary.Value} AND ";
                            break;
                        case API.IO.FilterEnum.GreaterOrEqual:
                            condition += $"{Employee.SALARY_PROPERTY} >= {Salary.Value} AND ";
                            break;
                        case API.IO.FilterEnum.Less:
                            condition += $"{Employee.SALARY_PROPERTY} < {Salary.Value} AND ";
                            break;
                        case API.IO.FilterEnum.LessOrEqual:
                            condition += $"{Employee.SALARY_PROPERTY} <= {Salary.Value} AND ";
                            break;
                    }
                }
                else
                {
                    condition += $"{Employee.SALARY_PROPERTY} = {Salary.Value} AND ";
                }
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
            if (string.IsNullOrEmpty(condition))
            {
                return "true";
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

        private static bool CheckForSqlInjection(string parameter)
        {
            return (parameter.Contains('\'') ||
                parameter.Contains('\"') ||
                parameter.Contains("--") ||
                parameter.Contains("&&") ||
                parameter.Contains("||") ||
                parameter.Contains(" OR ") ||
                parameter.Contains(" AND ") ||
                parameter.Contains('\\') ||
                parameter.Contains('/') ||
                parameter.Contains('(') ||
                parameter.Contains(')') ||
                parameter.Contains('%') ||
                parameter.Contains('_'));
        }
    }
}
