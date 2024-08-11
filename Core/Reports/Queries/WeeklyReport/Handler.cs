using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Reports.Queries.WeeklyReport
{
    public class Handler
    {
      /*  var startOfWeek = DateTime.UtcNow.StartOfWeek(DayOfWeek.Monday);
        var endOfWeek = startOfWeek.AddDays(7);

        var tasks = await _context.Tasks
            .Where(t => t.DueDate >= startOfWeek && t.DueDate < endOfWeek)
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
