using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Repository.Models;

namespace TaskManagement.Repository.Config
{
    public class EmployeeConfig : BaseDBConfig<Employee>, IEntityTypeConfiguration<TaskManagement.Repository.Models.Employee>
    {
        public void Configure(EntityTypeBuilder<TaskManagement.Repository.Models.Employee> builder)
        {
            BaseConfigure(builder);
            builder.HasMany(c => c.Tasks).WithOne(c => c.Employee).HasForeignKey(c => c.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired(true);
        }
    }
}