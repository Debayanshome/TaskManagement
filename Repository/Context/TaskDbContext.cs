using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagement.Repository.Config;
using TaskManagement.Repository.Models;
using TaskManagement.Shared.Repository;

namespace TaskManagement.Repository.Context
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<TaskDetail> TaskDetails => Set<TaskDetail>();
        public DbSet<Document> Documents => Set<Document>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeConfig).Assembly);


            modelBuilder.Entity<Document>().HasQueryFilter(GetGlobalFilterBase<Document>());
            modelBuilder.Entity<Employee>().HasQueryFilter(GetGlobalFilterBase<Employee>());
            modelBuilder.Entity<TaskDetail>().HasQueryFilter(GetGlobalFilterBase<TaskDetail>());


            base.OnModelCreating(modelBuilder);
        }

        private Expression<Func<T, bool>> GetGlobalFilterBase<T>() where T : EntityBase
        {
            return e => !e.IsDeleted;
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = 1;//dummy
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        entry.Entity.ModifiedBy = 1;
                        entry.Entity.ModifiedOn = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Entity.ModifiedBy = 1;
                        entry.Entity.ModifiedOn = DateTime.UtcNow;
                        break;
                }
            }
        }
    }
}
