using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dtos.AdmissionDocumentDtos;
using Dtos.DocumentPositionDtos;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views;
using Frontend.Views.DocumentPositionPages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Frontend.ViewModels;

public partial class DocumentPositionsViewModel : ObservableObject
{
    public ObservableCollection<DocumentPositionDto> DocumentPositions { get; private set; } = new();

    private readonly IDocumentPositionApiService _documentPositionApiService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private AdmissionDocumentDto _admissionDocument;

    public DocumentPositionsViewModel(
        IDocumentPositionApiService documentPositionApiService,
        INavigationService navigationService)
    {
        _documentPositionApiService = documentPositionApiService;
        _navigationService = navigationService;
    }

    public async Task LoadDataAsync(int admissionDocumentId)
    {
        DocumentPositions.Clear();

        IEnumerable<DocumentPositionDto> documentPositions =
            await _documentPositionApiService.GetDocumentPositionsUnderAdmisionDocumentAsync(admissionDocumentId);

        foreach (var documentPosition in documentPositions)
        {
            DocumentPositions.Add(documentPosition);
        }
    }

    [RelayCommand]
    public void NavigateToAdmissionDocumentsPage()
    {
        _navigationService.NavigateTo<AdmissionDocumentsPage>();
    }

    [RelayCommand]
    public void NavigateToDocumentPositionDetailsPage(DocumentPositionDto dto)
    {
        if (dto is null)
        {
            dto = new DocumentPositionDto();
        }

        dto.AdmissionDocumentId = AdmissionDocument.Id;

        _navigationService.NavigateTo<DocumentPositionDetailsPage>(dto);
    }
}
