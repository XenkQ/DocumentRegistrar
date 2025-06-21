using CommunityToolkit.Mvvm.Input;
using Frontend.Services;
using Frontend.Views;

namespace Frontend.ViewModels;

public partial class ContractorsViewModel
{
    private readonly INavigationService _navigationService;

    public ContractorsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    public void NavigateToMainPage()
    {
        _navigationService.NavigateTo<MainPage>();
    }

    [RelayCommand]
    public void NavigateToContractor()
    {
        _navigationService.NavigateTo<AdmissionDocumentsPage>();
    }
}
