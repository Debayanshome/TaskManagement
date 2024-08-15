using FluentValidation;
using TaskManagement.Core.Task.Commands.Shared;
using TaskManagement.Repository.Common.Enum;
using TaskManagement.Repository.Specifications.Task;
using TaskManagement.Shared.Repository.Interface;

namespace TaskManagement.Core.Task.Commands.Update
{
    public class Validator : TaskBaseValidator<CommandModel>
    {
        private readonly IReadRepository<Repository.Models.TaskDetail> _taskRepository;
        public Validator(IReadRepository<Repository.Models.TaskDetail> taskRepository, IReadRepository<Repository.Models.Employee> employeeRepository) : base(employeeRepository)
        {
            _taskRepository = taskRepository;
            RuleFor(x => x).NotEmpty().NotNull();
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0); 
            RuleFor(x => x.Status).Must(type => Enum.IsDefined(typeof(TaskStatusType), type)).WithMessage($"Invalid Task Status Type. Valid types are: {string.Join(", ", Enum.GetNames(typeof(TaskStatusType)))}.").MaximumLength(10);
            RuleFor(x => x).Custom(DocumentIdValidator).When(r => r.Documents != null && r.Documents.Count > 0 && r.Documents.Any(d => d.Id.HasValue));
            RuleForEach(x => x.Documents).ChildRules(doc =>
            {
                doc.RuleFor(y => y.Id).GreaterThan(0);
            }).When(x => x.Documents != null);
        }
        private void DocumentIdValidator(CommandModel updateData, ValidationContext<CommandModel> context)
        {
            var result = this._taskRepository.List(new TaskFetchSpecification(updateData.Documents.Where(v => v.Id.HasValue).Select(r => r.Id.Value).ToList(), updateData.Id));
            var inputDocumentIds = updateData.Documents.Where(c => c.Id.HasValue).Select(x => x.Id.Value).ToList();
            var dbDocumentIds = result.SelectMany(x => x.Documents.Select(r => r.Id)).ToList();

            var invalidIds = inputDocumentIds.Except(dbDocumentIds).ToList();
            if (invalidIds != null && invalidIds.Count > 0)
            {
                context.AddFailure("DocumentId", $"Invalid Document Id, the Document specified {string.Join(",", invalidIds)} doesn't belong to the current task!");
            }
        }
    }
}
