using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class HomeWorkAssignmentConfiguration : IEntityTypeConfiguration<HomeWorkAssignment>
    {
        public void Configure(EntityTypeBuilder<HomeWorkAssignment> builder)
        {
            builder.Property(b => b.RowVersion)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

        }
    }
}