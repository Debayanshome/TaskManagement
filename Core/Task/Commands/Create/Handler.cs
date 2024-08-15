using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManagement.Core.Task.Commands.Shared;
using TaskManagement.Core.Task.Shared;
using TaskManagement.Repository.Common.Enum;
using TaskManagement.Repository.Specifications.Task;
using TaskManagement.Shared.Repository.Interface;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Task.Commands.Create
{
    public class Handler : TaskBaseHandler, IRequestHandler<CommandModel, ValidationResult>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Repository.Models.TaskDetail> _taskDetailRepository;
        private readonly ILogger _logger;

        public Handler(IMapper mapper, IRepository<Repository.Models.TaskDetail> taskDetailRepository,  IRepository<Repository.Models.Employee> employeeRepository, ILogger<Handler> logger) : base(employeeRepository)
        {
            _mapper = mapper;
            _taskDetailRepository = taskDetailRepository;
            _logger = logger;
        }
        public async Task<ValidationResult> Handle(CommandModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Request initiated for task creation with task name: {task name}", request.Name);
            var taskDetail = new Repository.Models.TaskDetail()
            {
                Documents = new List<Repository.Models.Document>(),
                Status = TaskStatusType.Pending.ToString(),
                IsDeleted = false
            };

            if (request.Documents != null && request.Documents.Any())
            {
                request.Documents.ForEach(x => { x.Id = null; });
            }
          await  AssignValuesAsync(request, taskDetail, cancellationToken);
            await _taskDetailRepository.AddAsync(taskDetail, cancellationToken);
            var taskDetailSaved = await _taskDetailRepository.SaveChangesAsync(cancellationToken);
            if (taskDetailSaved <= 0)
            {
                _logger.LogError("Unable to save the task details with Id: {Id}", taskDetail.Id);
                return new InvalidValidationResult("Summary", "There was a server error saving 'Task Details Data'");
            }

            taskDetail = await this._taskDetailRepository.FirstOrDefaultAsync(new TaskFetchSpecification(taskDetail.Id).NoTracking(), cancellationToken);
            _logger.LogInformation("Successfully created the task with Id: {Id}", taskDetail.Id);
            return new CreatedResult(_mapper.Map<TaskResponseModel>(taskDetail));
        }
    }

}
