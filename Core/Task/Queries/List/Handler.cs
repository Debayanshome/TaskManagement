using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManagement.Core.Common;
using TaskManagement.Core.Task.Shared;
using TaskManagement.Repository.Specifications.Task;
using TaskManagement.Shared.Repository.Interface;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Task.Queries.List
{
   
    public class Handler : IRequestHandler<QueryModel, ValidationResult>
    {
        private readonly IReadRepository<Repository.Models.TaskDetail> _taskDetailRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public Handler(IReadRepository<Repository.Models.TaskDetail> taskDetailRepository, IMapper mapper, ILogger<Handler> logger)
        {
            this._taskDetailRepository = taskDetailRepository;
            this._mapper = mapper;
            _logger = logger;
        }
        public async Task<ValidationResult> Handle(QueryModel query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching the user list");
            var result = await this._taskDetailRepository.GetPagninated(new PaginatedTaskSpecification(query, Utility.GetSeparatedValues(query.StatusFilter)), query);
            _logger.LogInformation("Successfully fetched the user list");
            return new ValidObjectResult(_mapper.Map<PaginatedResponseModel<TaskResponseModel>>(result));
        }
    }
}
