using CommunityToolkit.Mvvm.Input;
using Dtos.ContractorsDtos;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views;
using Frontend.Views.ContractorPages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Frontend.ViewModels;

public partial class ContractorsViewModel
{
    public ObservableCollection<ContractorDto> Contractors { get; private set; } = new();
    private readonly IContractorApiService _contractorApiService;
    private readonly INavigationService _navigationService;

    public ContractorsViewModel(IContractorApiService contractorApiService, INavigationService navigationService)
    {
        _contractorApiService = contractorApiService;
        _navigationService = navigationService;
    }

    public async Task LoadContractorsAsync()
    {
        Contractors.Clear();

        IEnumerable<ContractorDto> contractors = await _contractorApiService.GetContractorsAsync();

        foreach (var contractor in contractors)
        {
            Contractors.Add(contractor);
        }
    }

    [RelayCommand]
    public void NavigateToMainPage()
    {
        _navigationService.NavigateTo<MainPage>();
    }

    [RelayCommand]
    public void NavigateToContractorDetails(ContractorDto contractorDto = null)
    {
        _navigationService.NavigateTo<ContractorDetailsPage>(contractorDto);
    }
}
