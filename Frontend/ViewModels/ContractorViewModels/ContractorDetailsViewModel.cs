using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dtos.ContractorsDtos;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views;
using System.Threading.Tasks;

namespace Frontend.ViewModels.ContractorViewModels;

public partial class ContractorDetailsViewModel : ViewModelBase
{
    private readonly IContractorApiService _contractorApiService;

    [ObservableProperty]
    private ContractorDto _contractor = new();

    public ContractorDetailsViewModel(
        IContractorApiService contractorApiService,
        INavigationService navigationService) : base(navigationService)
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
        if (IsEditMode)
        {
            await _contractorApiService.UpdateContractorAsync(Contractor.Id, new UpdateContractorDto()
            {
                Name = Contractor.Name,
                Symbol = Contractor.Symbol
            });
        }
        else
        {
            await _contractorApiService.CreateContractorAsync(new CreateContractorDto()
            {
                Name = Contractor.Name,
                Symbol = Contractor.Symbol
            });
        }

        _navigationService.NavigateTo<ContractorsPage>();
    }
}
