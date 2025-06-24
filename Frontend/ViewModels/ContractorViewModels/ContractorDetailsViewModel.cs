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

    [ObservableProperty]
    private ContractorDto _contractor = new();

    public ContractorDetailsViewModel(
        IContractorApiService contractorApiService,
        IDialogService dialogService,
        INavigationService navigationService) : base(dialogService, navigationService)
    {
        _contractorApiService = contractorApiService;
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
                await _contractorApiService.UpdateContractorAsync(Contractor.Id, updateContractor);
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
                await _contractorApiService.CreateContractorAsync(createContractor);
            }
        }

        if (canProceedWithObject)
        {
            _navigationService.NavigateTo<ContractorsPage>();
        }
    }
}
