using CommunityToolkit.Mvvm.Input;
using Dtos.DocumentPositionDtos;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Frontend.ViewModels;

public partial class DocumentPositionsViewModel
{
    public ObservableCollection<DocumentPositionDto> DocumentPositions { get; private set; } = new();
    private readonly IDocumentPositionApiService _documentPositionApiService;
    private readonly INavigationService _navigationService;

    public DocumentPositionsViewModel(
        IDocumentPositionApiService documentPositionApiService,
        INavigationService navigationService)
    {
        _documentPositionApiService = documentPositionApiService;
        _navigationService = navigationService;
    }

    public async Task LoadDocumentPositionsAsync(int admissionDocumentId)
    {
        DocumentPositions.Clear();

        IEnumerable<DocumentPositionDto> documentPositions =
            await _documentPositionApiService.GetDocumentPositionsAsync(admissionDocumentId);

        foreach (var documentPosition in documentPositions)
        {
            DocumentPositions.Add(documentPosition);
        }
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
