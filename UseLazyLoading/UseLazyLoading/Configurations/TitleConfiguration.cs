using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UseLazyLoading.Entities;

namespace UseLazyLoading.Constructions
{
    internal class TitleConfiguration : IEntityTypeConfiguration<Title>
    {
        public void Configure(EntityTypeBuilder<Title> builder)
        {
            builder.ToTable(nameof(Title)).HasKey(t => t.TitleId);
            builder.Property(t => t.TitleId).HasColumnName("TitleId").IsRequired().HasColumnType("int").ValueGeneratedOnAdd();
            builder.Property(t => t.Name).HasColumnName("Name").IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
        }
    }
}
