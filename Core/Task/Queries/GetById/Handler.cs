using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManagement.Shared.Repository.Interface;
using TaskManagement.Shared.Web.Results;
using TaskManagement.Repository.Specifications.Task;
using TaskManagement.Core.Task.Shared;

namespace TaskManagement.Core.Task.Queries.GetById
{
    public class Handler : IRequestHandler<QueryModel, ValidationResult>
    {
        private readonly IReadRepository<Repository.Models.TaskDetail> _taskDetailReadRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public Handler(IReadRepository<Repository.Models.TaskDetail> taskDetailReadRepository, IMapper mapper, ILogger<Handler> logger)
        {
            this._taskDetailReadRepository = taskDetailReadRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<ValidationResult> Handle(QueryModel query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching the Task Details with Id: {Id}", query.Id);
            var taskDetail = await this._taskDetailReadRepository.FirstOrDefaultAsync(new TaskFetchSpecification(query.Id), cancellationToken);
            if (taskDetail == null)
            {
                _logger.LogError("Task with Id: {Id} not found", query.Id);
                return new NotFoundValidationResult("TaskId", "Invalid Task Id");
            }

            _logger.LogInformation("Successfully fetched the task with Id: {Id}", taskDetail.Id);
            return new ValidObjectResult(_mapper.Map<TaskResponseModel>(taskDetail));
        }
    }
}
