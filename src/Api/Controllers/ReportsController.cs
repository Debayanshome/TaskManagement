using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskManagement.Api.Controllers
{
    [Route("[controller]")]
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
        [HttpGet("Weekly")]
        public async Task<IActionResult> GetWeeklyReport([FromQuery] TaskManagement.Core.Reports.Queries.WeeklyReport.QueryModel query)
        {
            return new ObjectResult(await _mediator.Send(query));
        }
       
        [HttpGet("Monthly")]
        public async Task<IActionResult> GetMonthlyReport([FromQuery] TaskManagement.Core.Reports.Queries.MonthlyReport.QueryModel query)
        {
            return new ObjectResult(await _mediator.Send(query));
        } 
        [HttpGet("Entire")]
        public async Task<IActionResult> GetEntireReport([FromQuery] TaskManagement.Core.Reports.Queries.EntireReport.QueryModel query)
        {
            return new ObjectResult(await _mediator.Send(query));
        }
        [HttpGet("ByEmployeeId/{empId}")]
        public async Task<IActionResult> GetEntireReport(int empId)
        {
            return new ObjectResult(await _mediator.Send(new TaskManagement.Core.Reports.Queries.ByEmployeeId.QueryModel { EmployeeId = empId}));
        }
    }
}
