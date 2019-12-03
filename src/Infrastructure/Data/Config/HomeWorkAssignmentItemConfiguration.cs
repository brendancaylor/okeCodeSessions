using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class HomeWorkAssignmentItemConfiguration : IEntityTypeConfiguration<HomeWorkAssignmentItem>
    {
        public void Configure(EntityTypeBuilder<HomeWorkAssignmentItem> builder)
        {
            builder.Property(b => b.Sentence)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(b => b.Word)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.RowVersion)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

        }
    }
}
