using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMediator _mediator;

        public EmployeeController(ILogger<EmployeeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        #region GET
        [ProducesResponseType(typeof(TaskManagement.Core.Task.Shared.EmployeeResponseModel), 200)]
        [HttpGet("{empId}")]
        public async Task<IActionResult> GetByIdAsync(int empId)
        {
            return new ObjectResult(await _mediator.Send(new TaskManagement.Core.Employee.Queries.GetById.QueryModel { Id = empId }));
        }

        #endregion
        #region POST

        [ProducesResponseType(typeof(TaskManagement.Core.Task.Shared.EmployeeResponseModel), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TaskManagement.Core.Employee.Commands.Create.CommandModel command)
        {
            return new ObjectResult(await _mediator.Send(command));
        }

        #endregion


        #region PUT

        [ProducesResponseType(typeof(TaskManagement.Core.Task.Shared.EmployeeResponseModel), 200)]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] TaskManagement.Core.Employee.Commands.Update.CommandModel command)
        {
            return new ObjectResult(await _mediator.Send(command));
        }

        #endregion
    }
}
