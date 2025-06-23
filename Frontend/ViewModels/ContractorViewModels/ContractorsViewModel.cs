using CommunityToolkit.Mvvm.Input;
using Dtos.ContractorsDtos;
using Frontend.Helpers;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views;
using Frontend.Views.ContractorPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Frontend.ViewModels;

public partial class ContractorsViewModel : ViewModelBase
{
    public ObservableCollection<ContractorDto> Contractors { get; private set; } = new();
    private readonly IContractorApiService _contractorApiService;

    public ContractorsViewModel(
        IContractorApiService contractorApiService,
        INavigationService navigationService) : base(navigationService)
    {
        _contractorApiService = contractorApiService;
    }

    public async Task LoadDataAsync()
    {
        IsLoading = true;

        Contractors.Clear();

        IEnumerable<ContractorDto> contractors =
            await ApiHelper.SafeApiCallAsync(
                () => _contractorApiService.GetContractorsAsync(),
                error => Console.WriteLine(error)) ?? new List<ContractorDto>();

        foreach (var contractor in contractors)
        {
            Contractors.Add(contractor);
        }

        IsLoading = false;
    }

    [RelayCommand]
    public void NavigateToMainPage()
    {
        _navigationService.NavigateTo<MainPage>();
    }

    [RelayCommand]
    public void NavigateToContractorDetailsPage(ContractorDto dto = null)
    {
        _navigationService.NavigateTo<ContractorDetailsPage>(dto);
    }
}
