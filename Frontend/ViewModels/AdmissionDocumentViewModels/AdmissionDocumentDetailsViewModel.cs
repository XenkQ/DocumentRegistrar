using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dtos.AdmissionDocumentDtos;
using Dtos.ContractorsDtos;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Frontend.ViewModels.AdmissionDocumentViewModels;

public partial class AdmissionDocumentDetailsViewModel : ObservableObject
{
    public ObservableCollection<ContractorDto> Contractors { get; set; } = new();

    private readonly IAdmissionDocumentApiService _admissionDocumentApiService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private AdmissionDocumentDto _admissionDocument = new() { Date = DateOnly.FromDateTime(DateTime.Today) };

    [ObservableProperty]
    private ContractorDto _selectedContractor;

    public AdmissionDocumentDetailsViewModel(
        IAdmissionDocumentApiService admissionDocumentApiService,
        INavigationService navigationService)
    {
        _admissionDocumentApiService = admissionDocumentApiService;
        _navigationService = navigationService;
    }

    public async Task LoadDataAsync()
    {
        Contractors.Clear();

        IEnumerable<ContractorDto> contractors = await _admissionDocumentApiService.GetContractorsAsync();

        foreach (var contractor in contractors)
        {
            if (contractor.Id == AdmissionDocument?.ContractorId)
            {
                SelectedContractor = contractor;
            }

            Contractors.Add(contractor);
        }
    }

    public bool IsEditMode => AdmissionDocument.Id != default;

    [RelayCommand]
    public void NavigateToAdmissionDocumentsPage()
    {
        _navigationService.NavigateTo<AdmissionDocumentsPage>();
    }

    [RelayCommand]
    public async void OnAdmissionDocumentSave()
    {
        if (IsEditMode)
        {
            await _admissionDocumentApiService.UpdateAdmissionDocumentAsync(AdmissionDocument.Id, new UpdateAdmissionDocumentDto()
            {
                Date = AdmissionDocument.Date,
                Symbol = AdmissionDocument.Symbol,
                ContractorId = SelectedContractor?.Id ?? AdmissionDocument.ContractorId
            });
        }
        else
        {
            await _admissionDocumentApiService.CreateAdmissionDocumentAsync(new CreateAdmissionDocumentDto()
            {
                Date = AdmissionDocument.Date,
                Symbol = AdmissionDocument.Symbol,
                ContractorId = SelectedContractor?.Id ?? AdmissionDocument.ContractorId
            });
        }

        _navigationService.NavigateTo<AdmissionDocumentsPage>();
    }
}
