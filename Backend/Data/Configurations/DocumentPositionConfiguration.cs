using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data.Configurations;

internal class DocumentPositionConfiguration : IEntityTypeConfiguration<DocumentPosition>
{
    public void Configure(EntityTypeBuilder<DocumentPosition> builder)
    {
        builder
            .Property(p => p.NameOfProduct)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(p => p.MeasurementUnit)
            .IsRequired()
            .HasMaxLength(15);
    }
}
