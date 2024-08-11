using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Common;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {


        private readonly ILogger<TaskManagementController> _logger;
        private readonly IMediator _mediator;
        public ReportsController(ILogger<TaskManagementController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet("weekly")]
        public async Task<IActionResult> GetWeeklyReport([FromQuery] TaskManagement.Core.Reports.Queries.WeeklyReport.QueryModel query)
        {
            return new ObjectResult(await _mediator.Send(query));
        }
       
        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyReport([FromQuery] TaskManagement.Core.Reports.Queries.MonthlyReport.QueryModel query)
        {
            return new ObjectResult(await _mediator.Send(query));
        }
    }
}
