using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dtos.AdmissionDocumentDtos;
using Dtos.CreateDocumentTypeDtos;
using Dtos.DocumentPositionDtos;
using Frontend.Helpers;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views;
using Frontend.Views.DocumentPositionPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.ViewModels.DocumentPositionViewModel;

public partial class DocumentPositionDetailsViewModel : ObjectValidationalViewModel
{
    public ObservableCollection<DocumentPositionTypeDto> DocumentPositionTypes { get; set; } = new();

    private readonly IDocumentPositionApiService _documentPositionApiService;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private DocumentPositionDto _documentPosition = new();

    [ObservableProperty]
    private DocumentPositionTypeDto? _selectedDocumentPositionType = null;

    public DocumentPositionDetailsViewModel(
        IDocumentPositionApiService documentPositionApiService,
        IDialogService dialogService,
        INavigationService navigationService) : base(dialogService, navigationService)
    {
        _documentPositionApiService = documentPositionApiService;
        _dialogService = dialogService;
    }

    public bool IsEditMode => DocumentPosition.Id != default;

    public async Task LoadDataAsync()
    {
        IsLoading = true;

        DocumentPositionTypes.Clear();

        DocumentPositionTypeDto? selectedDocumentPositionType = null;

        IEnumerable<DocumentPositionTypeDto> documentPositionTypes =
            await ApiHelper.SafeApiCallAsync(
                () => _documentPositionApiService.GetDocumentPositionTypesAsync(),
                (error, message) => _dialogService.ShowErrorMessage(message, error))
            ?? new List<DocumentPositionTypeDto>();

        foreach (var documentPositionType in documentPositionTypes)
        {
            if (documentPositionType.Name == _documentPosition?.DocumentPositionTypeName)
            {
                selectedDocumentPositionType = documentPositionType;
            }

            DocumentPositionTypes.Add(documentPositionType);
        }

        if (selectedDocumentPositionType is null)
        {
            SelectedDocumentPositionType = documentPositionTypes.Count() > 0 ? DocumentPositionTypes.First() : null;
        }
        else
        {
            SelectedDocumentPositionType = selectedDocumentPositionType;
        }

        IsLoading = false;
    }

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
                UnitPrice = DocumentPosition.UnitPrice,
                AdmissionDocumentId = DocumentPosition.AdmissionDocumentId
            };

            if (SelectedDocumentPositionType != null)
            {
                updateDocumentPosition.DocumentPositionTypeId = SelectedDocumentPositionType.Id;
            }

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
                UnitPrice = DocumentPosition.UnitPrice,
                AdmissionDocumentId = DocumentPosition.AdmissionDocumentId
            };

            if (SelectedDocumentPositionType != null)
            {
                createDocumentPosition.DocumentPositionTypeId = SelectedDocumentPositionType.Id;
            }

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
