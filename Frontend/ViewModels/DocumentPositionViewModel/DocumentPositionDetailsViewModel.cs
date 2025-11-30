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

public partial class DocumentPositionDetailsViewModel : ObjectValidationalViewModel
{
    private readonly IDocumentPositionApiService _documentPositionApiService;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private DocumentPositionDto _documentPosition = new();

    public DocumentPositionDetailsViewModel(
        IDocumentPositionApiService documentPositionApiService,
        IDialogService dialogService,
        INavigationService navigationService) : base(dialogService, navigationService)
    {
        _documentPositionApiService = documentPositionApiService;
        _dialogService = dialogService;
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
        bool canProceedWithObject = false;

        if (IsEditMode)
        {
            var updateDocumentPosition = new UpdateDocumentPositionDto()
            {
                NameOfProduct = DocumentPosition.NameOfProduct,
                MeasurementUnit = DocumentPosition.MeasurementUnit,
                Quantity = DocumentPosition.Quantity,
                AdmissionDocumentId = DocumentPosition.AdmissionDocumentId
            };

            canProceedWithObject = ValidationHelper.ValidateObject(
                updateDocumentPosition,
                ShowValidationErrorsDialogBox);

            if (canProceedWithObject)
            {
                await ApiHelper.SafeApiCallAsync(
                    () => _documentPositionApiService.UpdateDocumentPositionAsync(DocumentPosition.Id, updateDocumentPosition),
                    (error, _) => _dialogService.ShowErrorMessage("Can't update document position", error)
                );
            }
        }
        else
        {
            var createDocumentPosition = new CreateDocumentPositionDto()
            {
                NameOfProduct = DocumentPosition.NameOfProduct,
                MeasurementUnit = DocumentPosition.MeasurementUnit,
                Quantity = DocumentPosition.Quantity,
                AdmissionDocumentId = DocumentPosition.AdmissionDocumentId
            };

            canProceedWithObject = ValidationHelper.ValidateObject(
                createDocumentPosition,
                ShowValidationErrorsDialogBox);

            if (canProceedWithObject)
            {
                await ApiHelper.SafeApiCallAsync(
                    () => _documentPositionApiService.CreateDocumentPositionAsync(createDocumentPosition),
                    (error, _) => _dialogService.ShowErrorMessage("Can't create document position", error)
                );
            }
        }

        if (canProceedWithObject)
        {
            await NavigateToDocumentPositionsUnderRelatedAdmissionDocument();
        }
    }

    private async Task NavigateToDocumentPositionsUnderRelatedAdmissionDocument()
    {
        AdmissionDocumentDto? relatedAdmissionDocument =
            await ApiHelper.SafeApiCallAsync(
                () => _documentPositionApiService.GetAdmissionDocumentAsync(DocumentPosition.AdmissionDocumentId),
                (error, message) =>
                {
                    _dialogService.ShowErrorMessage(message, error);
                    _navigationService.NavigateTo<AdmissionDocumentsPage>();
                });

        if (relatedAdmissionDocument is not null)
        {
            _navigationService.NavigateTo<DocumentPositionsPage>(relatedAdmissionDocument);
        }
    }
}
