﻿using CommunityToolkit.Mvvm.Input;
using Frontend.Services;
using Frontend.Views;

namespace Frontend.ViewModels;

public partial class MainViewModel
{
    private readonly INavigationService _navigationService;

    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private void NavigateToContractors()
    {
        _navigationService.NavigateTo<ContractorsPage>();
    }

    [RelayCommand]
    private void NavigateToAdmissionDocuments()
    {
        _navigationService.NavigateTo<AdmissionDocumentsPage>();
    }
}
