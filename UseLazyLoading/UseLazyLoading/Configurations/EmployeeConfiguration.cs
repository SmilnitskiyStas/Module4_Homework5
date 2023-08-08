using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UseLazyLoading.Entities;

namespace UseLazyLoading.Constructions
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees").HasKey(e => e.EmployeeId);
            builder.Property(e => e.EmployeeId).HasColumnName("EmployeeId").IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.FirstName).HasColumnName("FirstName").IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(e => e.LastName).HasColumnName("LastName").IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(e => e.HiredDate).HasColumnName("HiredDate").IsRequired().HasColumnType("datetime2");
            builder.Property(e => e.DateOfBirth).HasColumnName("DateOfBirth").IsRequired().HasColumnType("date");
            builder.Property(e => e.OfficeId).HasColumnName("OfficeId").IsRequired().HasColumnType("int");
            builder.Property(e => e.TitleId).HasColumnName("TitleId").IsRequired().HasColumnType("int");

            builder.HasOne(o => o.Office)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Title)
                .WithMany(e => e.Employees)
                .HasForeignKey(t => t.TitleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
