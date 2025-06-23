using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dtos.AdmissionDocumentDtos;
using Dtos.ContractorsDtos;
using Frontend.Helpers;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.ViewModels.AdmissionDocumentViewModels;

public partial class AdmissionDocumentDetailsViewModel : ViewModelBase
{
    public ObservableCollection<ContractorDto> Contractors { get; set; } = new();

    private readonly IAdmissionDocumentApiService _admissionDocumentApiService;

    [ObservableProperty]
    private AdmissionDocumentDto _admissionDocument = new() { Date = DateOnly.FromDateTime(DateTime.Today) };

    [ObservableProperty]
    private ContractorDto _selectedContractor;

    public AdmissionDocumentDetailsViewModel(
        IAdmissionDocumentApiService admissionDocumentApiService,
        INavigationService navigationService) : base(navigationService)
    {
        _admissionDocumentApiService = admissionDocumentApiService;
    }

    public async Task LoadDataAsync()
    {
        IsLoading = true;

        Contractors.Clear();

        ContractorDto? selectedContractor = null;

        IEnumerable<ContractorDto> contractors =
            await ApiHelper.SafeApiCallAsync(
                () => _admissionDocumentApiService.GetContractorsAsync(),
                error => Console.WriteLine(error)) ?? new List<ContractorDto>();

        foreach (var contractor in contractors)
        {
            if (contractor.Id == AdmissionDocument?.ContractorId)
            {
                selectedContractor = contractor;
            }

            Contractors.Add(contractor);
        }

        if (selectedContractor is null)
        {
            SelectedContractor = contractors.First();
        }
        else
        {
            SelectedContractor = selectedContractor;
        }

        IsLoading = false;
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
