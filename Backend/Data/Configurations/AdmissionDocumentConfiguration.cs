using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data.Configurations;

internal class AdmissionDocumentConfiguration : IEntityTypeConfiguration<AdmissionDocument>
{
    public void Configure(EntityTypeBuilder<AdmissionDocument> builder)
    {
        builder
            .HasOne(ad => ad.Contractor)
            .WithMany(c => c.AdmissionDocuments)
            .HasForeignKey(ad => ad.ContractorId)
            .IsRequired();

        builder
            .HasMany(ad => ad.DocumentPositions)
            .WithOne(dp => dp.AdmissionDocument)
            .HasForeignKey(dp => dp.AdmissionDocumentId)
            .IsRequired();

        builder
            .Property(p => p.Date)
            .IsRequired();

        builder
            .Property(p => p.Symbol)
            .IsRequired();

        builder
            .HasIndex(p => p.Symbol)
            .IsUnique();
    }
}
