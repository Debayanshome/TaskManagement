using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManagement.Core.Task.Commands.Shared;
using TaskManagement.Core.Task.Shared;
using TaskManagement.Repository.Specifications.Task;
using TaskManagement.Shared.Repository.Interface;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Task.Commands.Status
{
    public class Handler : IRequestHandler<CommandModel, ValidationResult>
    {
        private readonly IRepository<Repository.Models.TaskDetail> _taskDetailRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public Handler(IRepository<Repository.Models.TaskDetail> taskDetailRepository, IMapper mapper, ILogger<Handler> logger) 
        {
            _taskDetailRepository = taskDetailRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ValidationResult> Handle(CommandModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching the task with Id: {Id}", request.Id);
            var taskDetail = await this._taskDetailRepository.FirstOrDefaultAsync(new TaskFetchSpecification(request.Id), cancellationToken);
            if (taskDetail == null)
            {
                _logger.LogError("Task with Id: {Id} not found", request.Id);
                return new NotFoundValidationResult("TaskId", "Invalid Task Id");
            }

            _logger.LogInformation("Found the task with Id: {Id}", taskDetail.Id);
            taskDetail.Status = request.Status;
            if(request.Status.ToLower() == TaskManagement.Repository.Common.Enum.TaskStatusType.Completed.ToString().ToLower())
                taskDetail.CompletedDate = DateTime.UtcNow;
            await _taskDetailRepository.UpdateAsync(taskDetail, cancellationToken);

            var saved = await _taskDetailRepository.SaveChangesAsync(cancellationToken);
            if (saved <= 0)
            {
                _logger.LogError("Unable to save the task with Id: {Id}", taskDetail.Id);
                return new InvalidValidationResult("Summary", "There was a server error updating 'Task'");
            }

            taskDetail = await this._taskDetailRepository.FirstOrDefaultAsync(new TaskFetchSpecification(taskDetail.Id).NoTracking(), cancellationToken);

            _logger.LogInformation("Successfully updated the status for task with Id: {Id}", taskDetail.Id);
            return new ValidObjectResult(_mapper.Map<TaskResponseModel>(taskDetail));
        }
    }

}
