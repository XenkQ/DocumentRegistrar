using CommunityToolkit.Mvvm.Input;
using Frontend.Services;
using Frontend.Views;

namespace Frontend.ViewModels;

public partial class MainPageViewModel : ViewModelBase
{
    public bool CanShowDocumentPositionTypePanel
    {
        get => _userService.IsAdmin() || _userService.IsManager();
    }

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
        _navigationService.NavigateTo<AdmissionDocumentsPage>();
    }

    [RelayCommand]
    private void NavigateToLoginPage()
    {
        _navigationService.NavigateTo<LoginPage>();
    }
}
