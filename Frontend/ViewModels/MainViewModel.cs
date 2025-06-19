using CommunityToolkit.Mvvm.Input;
using Frontend.Services;
using Frontend.Views;

namespace Frontend.ViewModels;

internal partial class MainViewModel
{
    private readonly INavigationService _navigationService;

    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private void NavigateToContractors()
    {
        _navigationService.NavigateToPageByName(nameof(ContractorsPage));
    }

    [RelayCommand]
    private void NavigateToAdmissionDocuments()
    {
        _navigationService.NavigateToPageByName(nameof(AdmissionDocumentsPage));
    }
}
