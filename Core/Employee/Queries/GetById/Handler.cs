using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManagement.Core.Task.Shared;
using TaskManagement.Repository.Specifications.Employee;
using TaskManagement.Shared.Repository.Interface;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Employee.Queries.GetById
{
    public class Handler : IRequestHandler<QueryModel, ValidationResult>
    {
        private readonly IReadRepository<Repository.Models.Employee> _employeeReadRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public Handler(IReadRepository<Repository.Models.Employee> employeeReadRepository, IMapper mapper, ILogger<Handler> logger)
        {
            this._employeeReadRepository = employeeReadRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<ValidationResult> Handle(QueryModel query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching the Employee Details with Id: {Id}", query.Id);
            var employeeDetail = await this._employeeReadRepository.FirstOrDefaultAsync(new EmployeeFetchSpecification(query.Id), cancellationToken);
            if (employeeDetail == null)
            {
                _logger.LogError("Employee with Id: {Id} not found", query.Id);
                return new NotFoundValidationResult("EmployeeId", "Invalid Employee Id");
            }

            _logger.LogInformation("Successfully fetched the Employee with Id: {Id}", employeeDetail.Id);
            return new ValidObjectResult(_mapper.Map<EmployeeResponseModel>(employeeDetail));
        }
    }
}
