using CommunityToolkit.Mvvm.Input;
using Dtos.UserDtos;
using Frontend.Helpers;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views;
using Frontend.Views.UserPages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Frontend.ViewModels.UserViewModel;

public partial class UsersViewModel : ViewModelBase
{
    public ObservableCollection<UserDto> Users { get; private set; } = new();

    private readonly IUserApiService _userApiService;
    private readonly IDialogService _dialogService;

    public UsersViewModel(
        IUserApiService userApiService,
        IDialogService dialogService,
        INavigationService navigationService) : base(navigationService)
    {
        _userApiService = userApiService;
        _dialogService = dialogService;
    }

    public async Task LoadDataAsync()
    {
        IsLoading = true;

        Users.Clear();

        IEnumerable<UserDto> users =
            await ApiHelper.SafeApiCallAsync(
                () => _userApiService.GetUsersAsync(),
                (error, message) => _dialogService.ShowErrorMessage(message, error))
            ?? new List<UserDto>();

        foreach (var user in users)
        {
            Users.Add(user);
        }

        IsLoading = false;
    }

    [RelayCommand]
    public void NavigateToMainPage()
    {
        _navigationService.NavigateTo<MainPage>();
    }

    [RelayCommand]
    public void NavigateToUserDetailsPage(UserDto dto = null)
    {
        _navigationService.NavigateTo<UserDetailsPage>(dto);
    }
}
