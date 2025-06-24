using CommunityToolkit.Mvvm.Input;
using Dtos.AdmissionDocumentDtos;
using Frontend.Helpers;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views;
using Frontend.Views.AdmissionDocumentPages;
using Frontend.Views.DocumentPositionPages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Frontend.ViewModels;

public partial class AdmissionDocumentsViewModel : ViewModelBase
{
    public ObservableCollection<AdmissionDocumentDto> AdmissionDocuments { get; private set; } = new();

    private readonly IAdmissionDocumentApiService _admissionDocumentApiService;
    private readonly IDialogService _dialogService;

    public AdmissionDocumentsViewModel(
        IAdmissionDocumentApiService admissionDocumentApiService,
        IDialogService dialogService,
        INavigationService navigationService) : base(navigationService)
    {
        _admissionDocumentApiService = admissionDocumentApiService;
        _dialogService = dialogService;
    }

    public async Task LoadDataAsync()
    {
        IsLoading = true;

        AdmissionDocuments.Clear();

        IEnumerable<AdmissionDocumentDto> admissionDocuments =
            await ApiHelper.SafeApiCallAsync(
                () => _admissionDocumentApiService.GetAdmissionDocumentsAsync(),
                error => _dialogService.ShowMessageAsync("Can't retrive data from server", error))
            ?? new List<AdmissionDocumentDto>();

        foreach (var admissionDocument in admissionDocuments)
        {
            AdmissionDocuments.Add(admissionDocument);
        }

        IsLoading = false;
    }

    [RelayCommand]
    public void NavigateToMainPage()
    {
        _navigationService.NavigateTo<MainPage>();
    }

    [RelayCommand]
    public void NavigateToAdmissionDocumentDetailsPage(AdmissionDocumentDto dto = null)
    {
        _navigationService.NavigateTo<AdmissionDocumentDetailsPage>(dto);
    }

    [RelayCommand]
    public void NavigateToDocumentPositionsPage(AdmissionDocumentDto dto = null)
    {
        _navigationService.NavigateTo<DocumentPositionsPage>(dto);
    }
}
