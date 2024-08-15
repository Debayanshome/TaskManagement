using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManagement.Repository.Specifications.Task;
using TaskManagement.Shared.Repository.Interface;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Reports.Queries.MonthlyReport
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

        public async Task<ValidationResult> Handle(QueryModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching Report Data");

            var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1);

            var taskDetail = await this._taskDetailReadRepository.ListAsync(new TaskFetchSpecification(request.TaskId, startOfMonth, endOfMonth), cancellationToken);

            var report = taskDetail.GroupBy(t => t.EmployeeId)
                             .Select(g => new
                             {
                                 EmployeeName = taskDetail.Select(r => r.Employee).FirstOrDefault(c => c.Id == g.Key).Name,
                                 CompletedTasks = g.Count(t => t.Status.ToLower() == Repository.Common.Enum.TaskStatusType.Completed.ToString().ToLower()),
                                 TotalTasks = g.Count(),
                                 TotalHoursSpent = g.Sum(t => t.TotalHoursSpent)
                             }).ToList();
            _logger.LogInformation("Successfully fetched report data");
            return new ValidObjectResult(report);
        }
    }
}
