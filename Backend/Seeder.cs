using Backend.Data;
using Backend.Entities;
using Microsoft.AspNetCore.Identity;

namespace Backend;

public class Seeder
{
    private readonly AppDbContext _dbContext;

    public Seeder(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        if (_dbContext.Database.CanConnect())
        {
            if (!_dbContext.Roles.Any())
            {
                _dbContext.Roles.AddRange(GetInitialRoles());
                _dbContext.SaveChanges();
            }

            if (!_dbContext.Users.Any())
            {
                _dbContext.Users.AddRange(GetInitialUsers());
                _dbContext.SaveChanges();
            }

            if (!_dbContext.DocumentPositionTypes.Any())
            {
                _dbContext.DocumentPositionTypes.AddRange(GetInitialDocumentPositionTypes());
                _dbContext.SaveChanges();
            }
        }
    }

    private IEnumerable<Role> GetInitialRoles()
    {
        return
        [
            new Role()
            {
                Name = "Admin"
            },
            new Role()
            {
                Name = "User"
            },
            new Role()
            {
                Name = "Manager"
            },
        ];
    }

    private IEnumerable<User> GetInitialUsers()
    {
        IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();

        List<User> users = [
            new User()
            {
                FirstName = "Admin",
                LastName = "System",
                Role = _dbContext.Roles.First(r => r.Name == "Admin"),
                Email = "admin@start.system",
            },
            new User()
            {
                FirstName = "Manager",
                LastName = "System",
                Role = _dbContext.Roles.First(r => r.Name == "Manager"),
                Email = "manager@start.system",
            },
            new User()
            {
                FirstName = "User",
                LastName = "System",
                Role = _dbContext.Roles.First(r => r.Name == "User"),
                Email = "user@start.system",
            },
        ];

        foreach (var user in users)
        {
            user.PasswordHash = passwordHasher.HashPassword(user, "start");
        }

        return users;
    }

    public IEnumerable<DocumentPositionType> GetInitialDocumentPositionTypes()
    {
        return [
            new DocumentPositionType() {
                Name = "Product",
            },
            new DocumentPositionType() {
                Name = "Service"
            }
        ];
    }
}
