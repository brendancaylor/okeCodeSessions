using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(b => b.RoleName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.DisplayName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
