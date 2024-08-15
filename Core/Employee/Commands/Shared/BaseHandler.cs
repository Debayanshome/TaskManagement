using TaskManagement.Core.Task.Commands.Shared;

namespace TaskManagement.Core.Employee.Commands.Shared
{
    public abstract class BaseHandler
    {
        protected BaseHandler()
        {
            

        }
        protected void AssignValues(BaseCommandModel request, Repository.Models.Employee employeeDetail, CancellationToken cancellationToken)
        {
            employeeDetail.Name = request.Name;
        }
    }
}
