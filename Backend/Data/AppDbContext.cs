using Backend.Data.Configurations;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

internal class AppDbContext : DbContext
{
    public DbSet<AdmissionDocument> AdmissionDocuments { get; set; }
    public DbSet<Contractor> Contractors { get; set; }
    public DbSet<DocumentPosition> DocumentPositions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AdmissionDocumentConfiguration());
        modelBuilder.ApplyConfiguration(new ContractorConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentPositionConfiguration());
    }
}
