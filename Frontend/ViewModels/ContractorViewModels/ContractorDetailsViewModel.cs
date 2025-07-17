using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dtos.ContractorsDtos;
using Frontend.Helpers;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views;
using System.Threading.Tasks;

namespace Frontend.ViewModels.ContractorViewModels;

public partial class ContractorDetailsViewModel : ObjectValidationalViewModel
{
    private readonly IContractorApiService _contractorApiService;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private ContractorDto _contractor = new();

    public ContractorDetailsViewModel(
        IContractorApiService contractorApiService,
        IDialogService dialogService,
        INavigationService navigationService) : base(dialogService, navigationService)
    {
        _contractorApiService = contractorApiService;
        _dialogService = dialogService;
    }

    public bool IsEditMode => Contractor.Id != default;

    [RelayCommand]
    public void NavigateToContractorsPage()
    {
        _navigationService.NavigateTo<ContractorsPage>();
    }

    [RelayCommand]
    public async Task OnContractorSave()
    {
        bool canProceedWithObject = false;

        if (IsEditMode)
        {
            var updateContractor = new UpdateContractorDto()
            {
                Name = Contractor.Name,
                Symbol = Contractor.Symbol
            };

            canProceedWithObject = ValidationHelper.ValidateObject(
                updateContractor,
                ShowValidationErrorsDialogBox);

            if (canProceedWithObject)
            {
                await ApiHelper.SafeApiCallAsync(
                     () => _contractorApiService.UpdateContractorAsync(Contractor.Id, updateContractor),
                     error => _dialogService.ShowMessageAsync("Can't update contractor", error)
                );
            }
        }
        else
        {
            var createContractor = new CreateContractorDto()
            {
                Name = Contractor.Name,
                Symbol = Contractor.Symbol
            };

            canProceedWithObject = ValidationHelper.ValidateObject(
                createContractor,
                ShowValidationErrorsDialogBox);

            if (canProceedWithObject)
            {
                await ApiHelper.SafeApiCallAsync(
                     () => _contractorApiService.CreateContractorAsync(createContractor),
                     error => _dialogService.ShowMessageAsync("Can't create contractor", error)
                );
            }
        }

        if (canProceedWithObject)
        {
            _navigationService.NavigateTo<ContractorsPage>();
        }
    }
}
