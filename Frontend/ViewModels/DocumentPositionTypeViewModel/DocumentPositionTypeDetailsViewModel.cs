using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dtos.CreateDocumentTypeDtos;
using Frontend.Helpers;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views.DocumentPositionTypePages;
using System.Threading.Tasks;

namespace Frontend.ViewModels.DocumentPositionTypeViewModel;

public partial class DocumentPositionTypeDetailsViewModel : ObjectValidationalViewModel
{
    private readonly IDocumentPositionTypeApiService _documentPositionTypeApiService;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private DocumentPositionTypeDto _documentPositionType = new();

    public DocumentPositionTypeDetailsViewModel(
        IDocumentPositionTypeApiService documentPositionTypeApiService,
        IDialogService dialogService,
        INavigationService navigationService) : base(dialogService, navigationService)
    {
        _documentPositionTypeApiService = documentPositionTypeApiService;
        _dialogService = dialogService;
    }

    public bool IsEditMode => DocumentPositionType.Id != default;

    [RelayCommand]
    public void NavigateToDocumentPositionTypesPage()
    {
        _navigationService.NavigateTo<DocumentPositionTypesPage>();
    }

    [RelayCommand]
    public async Task OnDocumentPositionTypeSave()
    {
        bool canProceedWithObject = false;

        if (IsEditMode)
        {
            var updateDocumentPositionType = new DocumentPositionTypeDto()
            {
                Name = DocumentPositionType.Name,
            };

            canProceedWithObject = ValidationHelper.ValidateObject(
                updateDocumentPositionType,
                ShowValidationErrorsDialogBox);

            if (canProceedWithObject)
            {
                await ApiHelper.SafeApiCallAsync(
                     () => _documentPositionTypeApiService.UpdateDocumentPositionTypeAsync(DocumentPositionType.Id, updateDocumentPositionType),
                     error => _dialogService.ShowMessageAsync("Can't update documentPositionType", error)
                );
            }
        }
        else
        {
            var createDocumentPositionType = new DocumentPositionTypeDto()
            {
                Name = DocumentPositionType.Name,
            };

            canProceedWithObject = ValidationHelper.ValidateObject(
                createDocumentPositionType,
                ShowValidationErrorsDialogBox);

            if (canProceedWithObject)
            {
                await ApiHelper.SafeApiCallAsync(
                     () => _documentPositionTypeApiService.CreateDocumentPositionTypeAsync(createDocumentPositionType),
                     error => _dialogService.ShowMessageAsync("Can't create documentPositionType", error)
                );
            }
        }

        if (canProceedWithObject)
        {
            _navigationService.NavigateTo<DocumentPositionTypesPage>();
        }
    }
}
