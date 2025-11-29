using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(user => user.Role);

        builder.Property(builder => builder.FirstName)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(builder => builder.LastName)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(builder => builder.Email)
            .IsRequired();

        builder.Property(builder => builder.PasswordHash)
            .IsRequired();
    }
}
