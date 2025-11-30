using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dtos.UserDtos;
using Frontend.Helpers;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views.UserPages;
using System.Threading.Tasks;

namespace Frontend.ViewModels.UserViewModel;

public partial class UserDetailsViewModel : ObjectValidationalViewModel
{
    private readonly IUserApiService _userApiService;
    private readonly IAccountApiService _accountApiService;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private UserDto _user = new();

    [ObservableProperty]
    private string _password;

    [ObservableProperty]
    private int _roleId;

    public UserDetailsViewModel(
        IUserApiService userApiService,
        IAccountApiService accountApiService,
        IDialogService dialogService,
        INavigationService navigationService) : base(dialogService, navigationService)
    {
        _userApiService = userApiService;
        _accountApiService = accountApiService;
        _dialogService = dialogService;
    }

    public bool IsEditMode => User.Id != default;

    [RelayCommand]
    public void NavigateToUsersPage()
    {
        _navigationService.NavigateTo<UsersPage>();
    }

    [RelayCommand]
    public async Task OnUserSave()
    {
        bool canProceedWithObject = false;

        if (IsEditMode)
        {
            var updateUser = new UpdateUserDto()
            {
                FirstName = User.FirstName,
                LastName = User.LastName,
                Email = User.Email,
                Password = Password,
                RoleId = RoleId
            };

            canProceedWithObject = ValidationHelper.ValidateObject(
                updateUser,
                ShowValidationErrorsDialogBox);

            if (canProceedWithObject)
            {
                await ApiHelper.SafeApiCallAsync(
                    () => _userApiService.UpdateUserAsync(User.Id, updateUser),
                    (error, _) => _dialogService.ShowErrorMessage("Can't update user", error)
                );
            }
        }
        else
        {
            var registerUser = new RegisterUserDto()
            {
                Email = User.Email,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Password = Password,
                RoleId = RoleId
            };

            canProceedWithObject = ValidationHelper.ValidateObject(
                registerUser,
                ShowValidationErrorsDialogBox);

            if (canProceedWithObject)
            {
                await ApiHelper.SafeApiCallAsync(
                    () => _accountApiService.RegisterAsync(registerUser),
                    (error, _) => _dialogService.ShowErrorMessage("Can't create user", error)
                );
            }
        }

        if (canProceedWithObject)
        {
            _navigationService.NavigateTo<UsersPage>();
        }
    }
}
