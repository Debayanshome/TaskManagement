using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Shared.Repository;

namespace TaskManagement.Repository.Config
{
    public abstract class BaseDBConfig<T> where T : EntityBase
    {
        protected void BaseConfigure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
