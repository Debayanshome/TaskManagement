using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManagement.Repository.Specifications.Employee;
using TaskManagement.Shared.Repository.Interface;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Reports.Queries.ByEmployeeId
{
    public class Handler : IRequestHandler<QueryModel, ValidationResult>
    {
        private readonly IReadRepository<Repository.Models.Employee> _employeeDetailReadRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public Handler(IReadRepository<Repository.Models.Employee> employeeDetailReadRepository, IMapper mapper, ILogger<Handler> logger)
        {
            this._employeeDetailReadRepository = employeeDetailReadRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<ValidationResult> Handle(QueryModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching Report Data");


            var empDetail = await this._employeeDetailReadRepository.FirstOrDefaultAsync(new EmployeeFetchSpecification(request.EmployeeId, true), cancellationToken);
            if (empDetail == null)
            {
                _logger.LogError("Employee with Id: {Id} not found", request.EmployeeId);
                return new NotFoundValidationResult("EmployeeId", "Invalid Employee Id");
            }

            var report = new
            {
                EmployeeName = empDetail.Name,
                TaskNames = empDetail.Tasks.Select(t => t.Name).ToList(),
                CompletedTasks = empDetail.Tasks.Count(t => t.Status.Equals(Repository.Common.Enum.TaskStatusType.Completed.ToString(), StringComparison.OrdinalIgnoreCase)),
                TotalTasks = empDetail.Tasks.Count(),
                TotalHoursSpent = empDetail.Tasks.Sum(t => t.TotalHoursSpent)
            };
            _logger.LogInformation("Successfully fetched report data");
            return new ValidObjectResult(report);
        }
    }
}
