using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Common;

namespace TaskManagement.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskManagementController : ControllerBase
    {
        private readonly ILogger<TaskManagementController> _logger;
        private readonly IMediator _mediator;
        public TaskManagementController(ILogger<TaskManagementController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        #region GET

        [ProducesResponseType(typeof(TaskManagement.Core.Task.Shared.TaskResponseModel), 200)]
        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetByIdAsync(int taskId)
        {
            var taskdetails = await _mediator.Send(new TaskManagement.Core.Task.Queries.GetById.QueryModel { Id = taskId });
            return new ObjectResult(taskdetails);
        }

        [ProducesResponseType(typeof(PaginatedResponseModel<TaskManagement.Core.Task.Shared.TaskResponseModel>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery] TaskManagement.Core.Task.Queries.List.QueryModel query)
        {
            return new ObjectResult(await _mediator.Send(query));
        }

        #endregion

        #region POST

        [ProducesResponseType(typeof(TaskManagement.Core.Task.Shared.TaskResponseModel), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TaskManagement.Core.Task.Commands.Create.CommandModel command)
        {
            return new ObjectResult(await _mediator.Send(command));
        }

        #endregion

        #region PUT

        [ProducesResponseType(typeof(TaskManagement.Core.Task.Shared.TaskResponseModel), 200)]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] TaskManagement.Core.Task.Commands.Update.CommandModel command)
        {
            return new ObjectResult(await _mediator.Send(command));
        }
        #endregion
    }
}
