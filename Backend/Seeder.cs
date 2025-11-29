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
                IEnumerable<Role> roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                _dbContext.SaveChanges();
            }

            if (!_dbContext.Users.Any())
            {
                _dbContext.Users.Add(GetInitialUser());
                _dbContext.SaveChanges();
            }
        }
    }

    private IEnumerable<Role> GetRoles()
    {
        return
        [
            new Role()
            {
                Name = "Admin"
            },
        ];
    }

    private User GetInitialUser()
    {
        //Password for initial app admin
        IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();

        var user = new User()
        {
            FirstName = "System",
            LastName = "Administrator",
            Role = _dbContext.Roles.First(),
            Email = "-"
        };

        user.PasswordHash = passwordHasher.HashPassword(user, "admin");

        return user;
    }
}
