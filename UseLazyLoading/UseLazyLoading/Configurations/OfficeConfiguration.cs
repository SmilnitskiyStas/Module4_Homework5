using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UseLazyLoading.Entities;

namespace UseLazyLoading.Constructions
{
    internal class OfficeConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.ToTable(nameof(Office)).HasKey(o => o.OfficeId);
            builder.Property(o => o.OfficeId).HasColumnName(nameof(Office.OfficeId)).IsRequired().ValueGeneratedOnAdd();
            builder.Property(o => o.Title).HasColumnName("Title").IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            builder.Property(o => o.Location).HasColumnName(nameof(Office.Location)).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
        }
    }
}
