using Database.Entities;
using ServerCore.API.IO;
using ServerCore.Model;

namespace ServerCore.API.Handlers
{
    internal class EmployeeCreate : IHandler
    {
        public Employee Employee { get; private set; }
        public CoreException? CoreException { get; private set; }

        public EmployeeCreate(Employee employee)
        {
            Employee = employee;
        }

        public EmployeeCreate(CoreException coreException)
        {
            Employee = new();
            CoreException = coreException;
        }

        public Response ProcessRequest()
        {
            if (CoreException != null)
            {
                return new() { Exception = CoreException };
            }
            var database = new DatabaseInteractor();
            Employee.Id = database.CreateEmployee(new EmployeeFilterBuilder(Employee));
            return new()
            {
                ResponseObjects = new() { Employee }
            };
        }
    }
}
