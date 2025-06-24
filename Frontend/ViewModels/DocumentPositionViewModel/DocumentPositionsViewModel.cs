using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dtos.AdmissionDocumentDtos;
using Dtos.DocumentPositionDtos;
using Frontend.Helpers;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views;
using Frontend.Views.DocumentPositionPages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Frontend.ViewModels;

public partial class DocumentPositionsViewModel : ViewModelBase
{
    public ObservableCollection<DocumentPositionDto> DocumentPositions { get; private set; } = new();

    private readonly IDocumentPositionApiService _documentPositionApiService;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private AdmissionDocumentDto _admissionDocument;

    public DocumentPositionsViewModel(
        IDocumentPositionApiService documentPositionApiService,
        IDialogService dialogService,
        INavigationService navigationService) : base(navigationService)
    {
        _documentPositionApiService = documentPositionApiService;
        _dialogService = dialogService;
    }

    public async Task LoadDataAsync(int admissionDocumentId)
    {
        IsLoading = true;

        DocumentPositions.Clear();

        IEnumerable<DocumentPositionDto> documentPositions =
            await ApiHelper.SafeApiCallAsync(
                () => _documentPositionApiService.GetDocumentPositionsUnderAdmisionDocumentAsync(admissionDocumentId),
                error => _dialogService.ShowMessageAsync("Can't retrive data from server", error))
            ?? new List<DocumentPositionDto>();

        foreach (var documentPosition in documentPositions)
        {
            DocumentPositions.Add(documentPosition);
        }

        IsLoading = false;
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
