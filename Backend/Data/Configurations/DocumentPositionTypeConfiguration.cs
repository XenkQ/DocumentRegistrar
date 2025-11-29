using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data.Configurations;

public class DocumentPositionTypeConfiguration : IEntityTypeConfiguration<DocumentPositionType>
{
    public void Configure(EntityTypeBuilder<DocumentPositionType> builder)
    {
        builder.Property(dpt => dpt.Name)
            .HasMaxLength(25)
            .IsRequired();
    }
}
