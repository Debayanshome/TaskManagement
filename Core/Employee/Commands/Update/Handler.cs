using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManagement.Core.Employee.Commands.Shared;
using TaskManagement.Core.Task.Shared;
using TaskManagement.Repository.Specifications.Employee;
using TaskManagement.Shared.Repository.Interface;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Employee.Commands.Update
{
    public class Handler : BaseHandler, IRequestHandler<CommandModel, ValidationResult>
    {
        private readonly IRepository<Repository.Models.Employee> _employeeDetailRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public Handler(IRepository<Repository.Models.Employee> employeeDetailRepository, IMapper mapper, ILogger<Handler> logger) : base()
        {
            _employeeDetailRepository = employeeDetailRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ValidationResult> Handle(CommandModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching the Employee with Id: {Id}", request.Id);
            var employeeDetail = await this._employeeDetailRepository.FirstOrDefaultAsync(new EmployeeFetchSpecification(request.Id), cancellationToken);
            if (employeeDetail == null)
            {
                _logger.LogError("Employee with Id: {Id} not found", request.Id);
                return new NotFoundValidationResult("EmployeeId", "Invalid Employee Id");
            }

            _logger.LogInformation("Found the Employee with Id: {Id}", employeeDetail.Id);
            AssignValues(request, employeeDetail, cancellationToken);
            await _employeeDetailRepository.UpdateAsync(employeeDetail, cancellationToken);

            var saved = await _employeeDetailRepository.SaveChangesAsync(cancellationToken);
            if (saved <= 0)
            {
                _logger.LogError("Unable to save the Employee with Id: {Id}", employeeDetail.Id);
                return new InvalidValidationResult("Summary", "There was a server error updating 'Employee'");
            }

            employeeDetail = await this._employeeDetailRepository.FirstOrDefaultAsync(new EmployeeFetchSpecification(employeeDetail.Id).NoTracking(), cancellationToken);

            _logger.LogInformation("Successfully updated the Employee with Id: {Id}", employeeDetail.Id);
            return new ValidObjectResult(_mapper.Map<EmployeeResponseModel>(employeeDetail));
        }
    }
}
