using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dtos.ContractorsDtos;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views;
using System.Threading.Tasks;

namespace Frontend.ViewModels.ContractorViewModels;

public partial class ContractorDetailsViewModel : ObservableObject
{
    private readonly IContractorApiService _contractorApiService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private ContractorDto _contractor = new();

    public ContractorDetailsViewModel(
        IContractorApiService contractorApiService,
        INavigationService navigationService)
    {
        _contractorApiService = contractorApiService;
        _navigationService = navigationService;
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
