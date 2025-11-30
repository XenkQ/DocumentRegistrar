using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Frontend.Services;
using Frontend.Views;
using Frontend.Views.DocumentPositionTypePages;
using Frontend.Views.UserPages;
using System.Threading.Tasks;

namespace Frontend.ViewModels;

public partial class MainPageViewModel : ViewModelBase
{
    [ObservableProperty]
    public bool _canShowDocumentPositionTypePanel;

    [ObservableProperty]
    public bool _canShowUsersPanel;

    [ObservableProperty]
    public string _userName;

    private readonly IUserContextService _userService;

    public MainPageViewModel(IUserContextService userService, INavigationService navigationService)
        : base(navigationService)
    {
        _userService = userService;
    }

    [RelayCommand]
    private void NavigateToContractors()
    {
        _navigationService.NavigateTo<ContractorsPage>();
    }

    [RelayCommand]
    private void NavigateToAdmissionDocuments()
    {
        _navigationService.NavigateTo<AdmissionDocumentsPage>();
    }

    [RelayCommand]
    private void NavigateToDocumentPositionTypes()
    {
        _navigationService.NavigateTo<DocumentPositionTypesPage>();
    }

    [RelayCommand]
    private void NavigateToUsers()
    {
        _navigationService.NavigateTo<UsersPage>();
    }

    [RelayCommand]
    private void NavigateToLoginPage()
    {
        _navigationService.NavigateTo<LoginPage>();
    }

    public async Task LoadProperties()
    {
        IsLoading = true;

        if (_userService.User == null)
        {
            await _userService.LoadUserFromStorage();
        }

        CanShowDocumentPositionTypePanel = _userService.IsAdmin() || _userService.IsManager();

        CanShowUsersPanel = _userService.IsAdmin();

        UserName = _userService.User.Name;

        IsLoading = false;
    }
}
