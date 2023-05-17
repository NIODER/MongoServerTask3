using ServerCore.API.IO;
using ServerCore.Model;

namespace ServerCore.API.Handlers
{
    internal class EmployeeDelete : IHandler
    {
        private readonly string id;
        private readonly CoreException? coreException;

        public EmployeeDelete(string id)
        {
            this.id = id;
        }

        public EmployeeDelete(CoreException coreException)
        {
            id = string.Empty;
            this.coreException = coreException;
        }

        public Response ProcessRequest()
        {
            if (coreException != null)
            {
                return new() { Exception = coreException };
            }
            var database = new DatabaseInteractor();
            var filter = new EmployeeFilterBuilder().WithId(id);
            var employee = database.GetEmployee(filter);
            database.DeleteEmployee(filter);
            return new()
            {
                ResponseObjects = new() { employee }
            };
        }
    }
}
