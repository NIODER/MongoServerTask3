using Database.Entities;
using ServerCore.API.IO;
using ServerCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore.API.Handlers
{
    internal class EmployeeGet : IHandler
    {
        private string? id;
        private string? name;
        private int? salary;
        private int? salaryFilter;
        private string? address;
        private string? phone;
        private string? email;
        public int? page;
        public int? page_size;

        public EmployeeGet(RequestParameters requestParameters)
        {
            id = requestParameters.UrlParameters.GetValueOrDefault(DbEntity.ID_PROPERTY);
            name = requestParameters.UrlParameters.GetValueOrDefault(Employee.NAME_PROPERTY);
            if (int.TryParse(requestParameters.UrlParameters.GetValueOrDefault(Employee.SALARY_PROPERTY), out int salary_))
            {
                salary = salary_;
            }
            if (int.TryParse(requestParameters.UrlParameters.GetValueOrDefault(Config.SalaryFilterProperty), out int salary_filter))
            {
                salaryFilter = salary_filter;
            }
            address = requestParameters.UrlParameters.GetValueOrDefault(Employee.RESIDENTIAL_ADDRESS_PROPERTY);
            phone = requestParameters.UrlParameters.GetValueOrDefault(Employee.PHONE_PROPERTY);
            email = requestParameters.UrlParameters.GetValueOrDefault(Employee.EMAIL_PROPERTY);
            if (int.TryParse(requestParameters.UrlParameters.GetValueOrDefault(Config.PageNumberProperty), out int page_))
            {
                page = page_;
            }
            else
            {
                page = 0;
            }
            if (int.TryParse(requestParameters.UrlParameters.GetValueOrDefault(Config.PageSizeProperty), out int pageSize_))
            {
                page_size = pageSize_;
            }
            else
            {
                page_size = 10;
            }
        }

        private EmployeeFilterBuilder GetEmployeeFilter()
        {
            var employeeFilter = new EmployeeFilterBuilder();
            if (id != null)
                employeeFilter.WithId(id);

            if (name != null)
                employeeFilter.WithName(name);

            if (phone != null)
                employeeFilter.WithPhoneNumber(phone);

            if (address != null)
                employeeFilter.WithAddress(address);

            if (email != null)
                employeeFilter.WithEmail(email);

            if (salary != null)
            {
                if (salaryFilter != null)
                    employeeFilter.WithSalary(salary.Value, (FilterEnum)salaryFilter.Value);
                else
                    employeeFilter.WithSalary(salary.Value);
            }
            return employeeFilter;
        }

        public Response ProcessRequest()
        {
            var database = new DatabaseInteractor();
            try
            {
                var employees = database.GetEmployees(GetEmployeeFilter());
                return new() { ResponseObjects = employees.OfType<object>().ToList() };
            }
            catch (ArgumentException e)
            {
                var ex = CoreException.InvalidDataTypeException;
                ex.Message += $" {e.ParamName}";
                return new() { Exception = ex };
            }
        }
    }
}
