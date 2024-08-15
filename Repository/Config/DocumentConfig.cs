using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Repository.Models;

namespace TaskManagement.Repository.Config
{
    public class DocumentConfig : BaseDBConfig<Document>, IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            BaseConfigure(builder);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired(true);
            builder.Property(p => p.Type).HasMaxLength(50).IsRequired(true);
            builder.Property(p => p.Path).HasMaxLength(200).IsRequired(true);
        }
    }
}
