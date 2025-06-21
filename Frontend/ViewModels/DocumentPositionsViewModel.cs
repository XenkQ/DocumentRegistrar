using CommunityToolkit.Mvvm.Input;
using Frontend.Services;
using Frontend.Views;

namespace Frontend.ViewModels;

public partial class DocumentPositionsViewModel
{
    private readonly INavigationService _navigationService;

    public DocumentPositionsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    public void NavigateToAdmissionDocumentsPage()
    {
        _navigationService.NavigateTo<MainPage>();
    }

    [RelayCommand]
    public void NavigateToDocumentPosition()
    {
        _navigationService.NavigateTo<AdmissionDocumentsPage>();
    }
}
