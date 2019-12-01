using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{

    public class SubmittedHomeWorkConfiguration : IEntityTypeConfiguration<SubmittedHomeWork>
    {
        public void Configure(EntityTypeBuilder<SubmittedHomeWork> builder)
        {
            builder.Property(b => b.StudentName)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}
