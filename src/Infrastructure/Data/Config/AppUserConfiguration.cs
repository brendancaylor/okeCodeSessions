using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{

    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(b => b.IdentityId)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(b => b.Email)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(b => b.FirstName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(b => b.LastName)
                .IsRequired()
                .HasMaxLength(250);

        }
    }
}
