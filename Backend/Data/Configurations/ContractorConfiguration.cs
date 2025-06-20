using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data.Configurations;

internal class ContractorConfiguration : IEntityTypeConfiguration<Contractor>
{
    public void Configure(EntityTypeBuilder<Contractor> builder)
    {
        builder
            .Property(p => p.Symbol)
            .IsRequired();

        builder
            .HasIndex(p => p.Symbol)
            .IsUnique();

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
