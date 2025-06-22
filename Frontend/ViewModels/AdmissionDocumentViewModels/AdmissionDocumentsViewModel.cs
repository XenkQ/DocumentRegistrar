using CommunityToolkit.Mvvm.Input;
using Dtos.AdmissionDocumentDtos;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views;
using Frontend.Views.AdmissionDocumentPages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Frontend.ViewModels;

public partial class AdmissionDocumentsViewModel
{
    public ObservableCollection<AdmissionDocumentDto> AdmissionDocuments { get; private set; } = new();
    private readonly IAdmissionDocumentApiService _admissionDocumentApiService;
    private readonly INavigationService _navigationService;

    public AdmissionDocumentsViewModel(
        IAdmissionDocumentApiService admissionDocumentApiService,
        INavigationService navigationService)
    {
        _admissionDocumentApiService = admissionDocumentApiService;
        _navigationService = navigationService;
    }

    public async Task LoadAdmissionDocumentsAsync()
    {
        AdmissionDocuments.Clear();

        IEnumerable<AdmissionDocumentDto> admissionDocuments =
            await _admissionDocumentApiService.GetAdmissionDocumentsAsync();

        foreach (var admissionDocument in admissionDocuments)
        {
            AdmissionDocuments.Add(admissionDocument);
        }
    }

    [RelayCommand]
    public void NavigateToMainPage()
    {
        _navigationService.NavigateTo<MainPage>();
    }

    [RelayCommand]
    public void NavigateToCreateAdmissionDocument()
    {
        _navigationService.NavigateTo<AdmissionDocumentDetailsPage>();
    }
}
