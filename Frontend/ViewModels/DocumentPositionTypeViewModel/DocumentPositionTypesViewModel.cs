using CommunityToolkit.Mvvm.Input;
using Dtos.CreateDocumentTypeDtos;
using Frontend.Helpers;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views;
using Frontend.Views.DocumentPositionTypePages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Frontend.ViewModels.DocumentPositionTypeViewModel;

public partial class DocumentPositionTypesViewModel : ViewModelBase
{
    public ObservableCollection<DocumentPositionTypeDto> DocumentPositionTypes { get; private set; } = new();

    private readonly IDocumentPositionTypeApiService _documentPositionTypeApiService;
    private readonly IDialogService _dialogService;

    public DocumentPositionTypesViewModel(
        IDocumentPositionTypeApiService documentPositionTypeApiService,
        IDialogService dialogService,
        INavigationService navigationService) : base(navigationService)
    {
        _documentPositionTypeApiService = documentPositionTypeApiService;
        _dialogService = dialogService;
    }

    public async Task LoadDataAsync()
    {
        IsLoading = true;

        DocumentPositionTypes.Clear();

        IEnumerable<DocumentPositionTypeDto> documentPositionTypes =
            await ApiHelper.SafeApiCallAsync(
                () => _documentPositionTypeApiService.GetDocumentPositionTypesAsync(),
                (error, message) => _dialogService.ShowErrorMessage(message, error))
            ?? new List<DocumentPositionTypeDto>();

        foreach (var documentPositionType in documentPositionTypes)
        {
            DocumentPositionTypes.Add(documentPositionType);
        }

        IsLoading = false;
    }

    [RelayCommand]
    public void NavigateToMainPage()
    {
        _navigationService.NavigateTo<MainPage>();
    }

    [RelayCommand]
    public void NavigateToDocumentPositionTypeDetailsPage(DocumentPositionTypeDto dto = null)
    {
        _navigationService.NavigateTo<DocumentPositionTypeDetailsPage>(dto);
    }
}
