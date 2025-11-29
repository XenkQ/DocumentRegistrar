using Dtos.UserDtos;
using Frontend.Models;
using Frontend.Services;
using Frontend.Services.Api;
using System.Threading.Tasks;

namespace Frontend.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private readonly IAccountApiService _accountApiService;

    public LoginViewModel(IAccountApiService accountApiService, INavigationService navigationService)
        : base(navigationService)
    {
        _accountApiService = accountApiService;
    }

    public async Task<User> LoginAsync(LoginUserDto dto)
    {
        return await _accountApiService.LoginAsync(dto);
    }
}
