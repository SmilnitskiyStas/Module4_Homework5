using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UseLazyLoading.Entities;

namespace UseLazyLoading.Constructions
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable(nameof(Project)).HasKey(p => p.ProjectId);
            builder.Property(p => p.ProjectId).HasColumnName("ProjectId").IsRequired().HasColumnType("int").ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(p => p.Budget).HasColumnName("Budget").IsRequired().HasColumnType("money");
            builder.Property(p => p.StartedDate).HasColumnName("StartedDate").IsRequired().HasColumnType("datetime2(7)");
        }
    }
}
