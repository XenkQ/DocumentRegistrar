using Frontend.Models;
using System.Threading.Tasks;

namespace Frontend.Services;

public interface IUserService
{
    User User { get; }

    Task SetAndSaveUserAsync(User user);

    bool IsAdmin();

    bool IsManager();

    bool IsUser();
}

public class UserService : IUserService
{
    public User User { get; private set; } = new User();

    private readonly ILocalStorageService _localStorageService;

    public UserService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;

        SetUserFromStorage();
    }

    public bool IsAdmin() => User?.RoleName == "Admin";

    public bool IsManager() => User?.RoleName == "Manager";

    public bool IsUser() => User?.RoleName == "User";

    public async Task SaveUser(User user)
    {
        await _localStorageService.SaveAsync(LocalStorageService.Keys.User, user);
    }

    public async Task SetAndSaveUserAsync(User user)
    {
        if (user == null)
        {
            return;
        }

        await _localStorageService.SaveAsync(LocalStorageService.Keys.User, user);

        User = user;
    }

    private async Task SetUserFromStorage()
    {
        var user = await _localStorageService.ReadAsync<User>(LocalStorageService.Keys.User);

        if (user != null)
        {
            User = user;
        }
    }
}
