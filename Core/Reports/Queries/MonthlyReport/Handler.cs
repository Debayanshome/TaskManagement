using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Reports.Queries.MonthlyReport
{
    public class Handler
    {

        public Handler()
        {
                
        }
        /*  var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
          var endOfMonth = startOfMonth.AddMonths(1);

          var tasks = await _context.Tasks
              .Where(t => t.DueDate >= startOfMonth && t.DueDate < endOfMonth)
              .ToListAsync();

          var report = tasks.GroupBy(t => t.EmployeeId)
                            .Select(g => new
                            {
                                EmployeeId = g.Key,
                                CompletedTasks = g.Count(t => t.IsCompleted),
                                TotalTasks = g.Count()
                            });*/
    }
}
