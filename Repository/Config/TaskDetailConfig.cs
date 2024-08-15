using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Repository.Models;

namespace TaskManagement.Repository.Config
{
    public class TaskDetailConfig : BaseDBConfig<TaskDetail>, IEntityTypeConfiguration<TaskManagement.Repository.Models.TaskDetail>
    {
        public void Configure(EntityTypeBuilder<TaskManagement.Repository.Models.TaskDetail> builder)
        {
            BaseConfigure(builder);
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired(true);
            builder.Property(p => p.Details).HasMaxLength(5000).IsRequired(true);
            builder.Property(p => p.Notes).HasMaxLength(5000).IsRequired(false);
            builder.Property(p => p.StartDate).IsRequired(true);
            builder.Property(p => p.DueDate).IsRequired(true);
            builder.Property(p => p.CompletedDate).IsRequired(false);
            builder.Property(p => p.Status).HasMaxLength(10).IsRequired(true);
            builder.Property(p => p.Timezone).HasMaxLength(20).IsRequired(true);
        }
    }
}