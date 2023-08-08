using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UseLazyLoading.Entities;

namespace UseLazyLoading.Constructions
{
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable(nameof(Client)).HasKey(c => c.ClientId);
            builder.Property(c => c.ClientId).HasColumnName("ClientId").HasColumnType("int").ValueGeneratedOnAdd();
            builder.Property(c => c.FirstName).HasColumnName("FirstName").IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(c => c.LastName).HasColumnName("LastName").IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(c => c.DateOfBirth).HasColumnName("DateOfBirth").IsRequired().HasColumnType("datetime2");
            builder.Property(c => c.Email).HasColumnName("Email").IsRequired().HasColumnType("nvarchar").HasMaxLength(25);
        }
    }
}
