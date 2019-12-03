using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{

    public class YearClassConfiguration : IEntityTypeConfiguration<YearClass>
    {
        public void Configure(EntityTypeBuilder<YearClass> builder)
        {
            builder.Property(b => b.TeacherName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.YearClassName)
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
