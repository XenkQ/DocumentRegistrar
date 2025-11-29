using Backend.Data.Configurations;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class AppDbContext : DbContext
{
    public DbSet<AdmissionDocument> AdmissionDocuments { get; set; }
    public DbSet<Contractor> Contractors { get; set; }
    public DbSet<DocumentPosition> DocumentPositions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AdmissionDocumentConfiguration());
        modelBuilder.ApplyConfiguration(new ContractorConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentPositionConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }
}
