using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManagement.Core.Employee.Commands.Shared;
using TaskManagement.Core.Task.Shared;
using TaskManagement.Repository.Specifications.Employee;
using TaskManagement.Shared.Repository.Interface;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Employee.Commands.Create
{
    public class Handler : BaseHandler, IRequestHandler<CommandModel, ValidationResult>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Repository.Models.Employee> _employeeRepository;
        private readonly ILogger _logger;

        public Handler(IMapper mapper, IRepository<Repository.Models.Employee> employeeRepository, ILogger<Handler> logger) : base()
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }
        public async Task<ValidationResult> Handle(CommandModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Request initiated for Employee creation with Employee name: {employee name}", request.Name);
            var employeeDetail = new Repository.Models.Employee()
            {
                IsDeleted = false
            };

            AssignValues(request, employeeDetail, cancellationToken);
            await _employeeRepository.AddAsync(employeeDetail, cancellationToken);
            var employeeDetailSaved = await _employeeRepository.SaveChangesAsync(cancellationToken);
            if (employeeDetailSaved <= 0)
            {
                _logger.LogError("Unable to save the employee details with Id: {Id}", employeeDetail.Id);
                return new InvalidValidationResult("Summary", "There was a server error saving 'Employee Details Data'");
            }

            employeeDetail = await this._employeeRepository.FirstOrDefaultAsync(new EmployeeFetchSpecification(employeeDetail.Id).NoTracking(), cancellationToken);
            _logger.LogInformation("Successfully created the Employee with Id: {Id}", employeeDetail.Id);
            return new CreatedResult(_mapper.Map<EmployeeResponseModel>(employeeDetail));
        }
    }
}
