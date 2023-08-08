using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UseLazyLoading.Entities;

namespace UseLazyLoading.Constructions
{
    internal class EmployeeProjectConfiguration : IEntityTypeConfiguration<EmployeeProject>
    {
        public void Configure(EntityTypeBuilder<EmployeeProject> builder)
        {
            builder.ToTable(nameof(EmployeeProject)).HasKey(ep => ep.EmployeeProjectId);
            builder.Property(ep => ep.EmployeeProjectId).HasColumnName("EmployeeProjectId").IsRequired().ValueGeneratedOnAdd();
            builder.Property(ep => ep.Rate).HasColumnName("Rate").HasColumnType("money");
            builder.Property(ep => ep.StartedDate).HasColumnName("StartedDate").HasColumnType("datetime2(7)");
            builder.Property(ep => ep.EmployeeId).HasColumnName("EmployeeId").HasColumnType("int");
            builder.Property(ep => ep.ProjectId).HasColumnName("ProjectId").HasColumnType("int");

            builder.HasOne(e => e.Employee)
                .WithMany(ep => ep.EmployeeProjects)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Project)
                .WithMany(ep => ep.EmployeeProjects)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
