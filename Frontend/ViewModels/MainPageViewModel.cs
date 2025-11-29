using CommunityToolkit.Mvvm.Input;
using Frontend.Services;
using Frontend.Views;

namespace Frontend.ViewModels;

public partial class MainPageViewModel : ViewModelBase
{
    public MainPageViewModel(INavigationService navigationService)
        : base(navigationService)
    {
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
}
