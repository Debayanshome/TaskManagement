using TaskManagement.Core.Task.Commands.Shared;
using TaskManagement.Shared.Repository.Interface;

namespace TaskManagement.Core.Task.Commands.Create
{
    public class Validator : TaskBaseValidator<CommandModel>
    {
        public Validator(IReadRepository<Repository.Models.Employee> employeeRepository) : base(employeeRepository)
        {
                
        }
    }
}
