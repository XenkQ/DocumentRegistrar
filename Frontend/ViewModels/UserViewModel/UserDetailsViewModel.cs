using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dtos.RoleDto;
using Dtos.UserDtos;
using Frontend.Helpers;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views.UserPages;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.ViewModels.UserViewModel;

public partial class UserDetailsViewModel : ObjectValidationalViewModel
{
    public ObservableCollection<RoleDto> Roles { get; set; } = new();

    private readonly IUserApiService _userApiService;
    private readonly IAccountApiService _accountApiService;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private UserDto _user = new();

    [ObservableProperty]
    private RoleDto? _selectedRole;

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

    public async Task LoadDataAsync()
    {
        IsLoading = true;

        Roles.Clear();

        RoleDto? selectedRole = null;

        IEnumerable<RoleDto> roles =
            await ApiHelper.SafeApiCallAsync(
                () => _userApiService.GetAllRolesAsync(),
                (error, message) => _dialogService.ShowErrorMessage(message, error))
            ?? new List<RoleDto>();

        foreach (var role in roles)
        {
            if (role.Name == User?.RoleName)
            {
                selectedRole = role;
            }

            Roles.Add(role);
        }

        if (selectedRole is null)
        {
            SelectedRole = roles.Any() ? Roles.First() : null;
        }
        else
        {
            SelectedRole = selectedRole;
        }

        IsLoading = false;
    }

    [RelayCommand]
    public void NavigateToUsersPage()
    {
        _navigationService.NavigateTo<UsersPage>();
    }

    [RelayCommand]
    public async Task UserSave(object parameter)
    {
        if (parameter is PasswordBox passwordBox)
        {
            IsLoading = true;

            bool canProceedWithObject = false;

            string clearTextPassword = passwordBox.Password;

            if (IsEditMode)
            {
                var updateUser = new UpdateUserDto()
                {
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    Email = User.Email,
                    Password = clearTextPassword,
                    RoleId = SelectedRole.Id
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
                    Password = clearTextPassword,
                    RoleId = SelectedRole.Id
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

            IsLoading = false;
        }
    }
}
