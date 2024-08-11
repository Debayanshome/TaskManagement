using Microsoft.EntityFrameworkCore;
using TaskManagement.Repository.Models;

namespace TaskManagement.Repository.Context
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<TaskDetail> TaskDetails { get; set; }
        public DbSet<TaskLog> TaskLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
