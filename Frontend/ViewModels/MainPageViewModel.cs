using CommunityToolkit.Mvvm.Input;
using Frontend.Services;
using Frontend.Views;
using Frontend.Views.DocumentPositionTypePages;

namespace Frontend.ViewModels;

public partial class MainPageViewModel : ViewModelBase
{
    public bool CanShowDocumentPositionTypePanel { get => _userService.IsAdmin() || _userService.IsManager(); }
    public string UserName { get => _userService.User.Name; }

    private readonly IUserService _userService;

    public MainPageViewModel(IUserService userService, INavigationService navigationService)
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
    private void NavigateToLoginPage()
    {
        _navigationService.NavigateTo<LoginPage>();
    }
}
