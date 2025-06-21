using CommunityToolkit.Mvvm.Input;
using Frontend.Services;
using Frontend.Views;

namespace Frontend.ViewModels;

public partial class AdmissionDocumentsViewModel
{
    private readonly INavigationService _navigationService;

    public AdmissionDocumentsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    public void NavigateToMainPage()
    {
        _navigationService.NavigateTo<MainPage>();
    }

    [RelayCommand]
    public void NavigateToAdmissionDocument()
    {
        _navigationService.NavigateTo<AdmissionDocumentsPage>();
    }
}
