using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{

    public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            builder.Property(b => b.ClaimName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.DisplayName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
