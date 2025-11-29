using Frontend.Models;
using System.Threading.Tasks;

namespace Frontend.Services;

public interface IUserService
{
    User? User { get; set; }

    bool IsAdmin();

    bool IsManager();

    bool IsUser();
}

public class UserService : IUserService
{
    public User? User { get; set; }
    private readonly ILocalStorageService _localStorageService;

    public UserService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;

        SetUserFromStorage().Wait();
    }

    public bool IsAdmin() => User?.RoleName == "Admin";

    public bool IsManager() => User?.RoleName == "Manager";

    public bool IsUser() => User?.RoleName == "User";

    private async Task SetUserFromStorage()
    {
        User = await _localStorageService.ReadAsync<User>(LocalStorageService.Keys.User);
    }
}
