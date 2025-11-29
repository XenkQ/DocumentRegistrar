using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dtos.UserDtos;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views;
using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;

namespace Frontend.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly IAccountApiService _accountApiService;

    [ObservableProperty]
    private string _email;

    public LoginViewModel(IAccountApiService accountApiService, INavigationService navigationService)
        : base(navigationService)
    {
        _accountApiService = accountApiService;
    }

    [RelayCommand]
    public void NavigateToMainPage()
    {
        _navigationService.NavigateTo<MainPage>();
    }

    [RelayCommand]
    public async Task Login(object parameter)
    {
        if (parameter is PasswordBox passwordBox)
        {
            IsLoading = true;

            try
            {
                string clearTextPassword = passwordBox.Password;

                await _accountApiService.LoginAsync(new LoginUserDto()
                {
                    Email = Email,
                    Password = clearTextPassword
                });

                _navigationService.NavigateTo<MainPage>();
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
