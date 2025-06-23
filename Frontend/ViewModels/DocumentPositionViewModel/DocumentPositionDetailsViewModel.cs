using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dtos.AdmissionDocumentDtos;
using Dtos.DocumentPositionDtos;
using Frontend.Helpers;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views;
using Frontend.Views.DocumentPositionPages;
using System;
using System.Threading.Tasks;

namespace Frontend.ViewModels.DocumentPositionViewModel;

public partial class DocumentPositionDetailsViewModel : ViewModelBase
{
    private readonly IDocumentPositionApiService _documentPositionApiService;

    [ObservableProperty]
    private DocumentPositionDto _documentPosition = new();

    public DocumentPositionDetailsViewModel(
        IDocumentPositionApiService documentPositionApiService,
        INavigationService navigationService) : base(navigationService)
    {
        _documentPositionApiService = documentPositionApiService;
    }

    public bool IsEditMode => DocumentPosition.Id != default;

    [RelayCommand]
    public async Task NavigateToDocumentPositionsPage()
    {
        await NavigateToDocumentPositionsUnderRelatedAdmissionDocument();
    }

    [RelayCommand]
    public async Task OnDocumentPositionSave()
    {
        if (IsEditMode)
        {
            await _documentPositionApiService.UpdateDocumentPositionAsync(DocumentPosition.Id, new UpdateDocumentPositionDto()
            {
                NameOfProduct = DocumentPosition.NameOfProduct,
                MeasurementUnit = DocumentPosition.MeasurementUnit,
                Quantity = DocumentPosition.Quantity,
                AdmissionDocumentId = DocumentPosition.AdmissionDocumentId
            });
        }
        else
        {
            await _documentPositionApiService.CreateDocumentPositionAsync(new CreateDocumentPositionDto()
            {
                NameOfProduct = DocumentPosition.NameOfProduct,
                MeasurementUnit = DocumentPosition.MeasurementUnit,
                Quantity = DocumentPosition.Quantity,
                AdmissionDocumentId = DocumentPosition.AdmissionDocumentId
            });
        }

        await NavigateToDocumentPositionsUnderRelatedAdmissionDocument();
    }

    private async Task NavigateToDocumentPositionsUnderRelatedAdmissionDocument()
    {
        AdmissionDocumentDto? relatedAdmissionDocument =
            await ApiHelper.SafeApiCallAsync(
                () => _documentPositionApiService.GetAdmissionDocumentAsync(DocumentPosition.AdmissionDocumentId),
                error =>
                {
                    Console.WriteLine(error);
                    _navigationService.NavigateTo<AdmissionDocumentsPage>();
                });

        if (relatedAdmissionDocument is not null)
        {
            _navigationService.NavigateTo<DocumentPositionsPage>(relatedAdmissionDocument);
        }
    }
}
