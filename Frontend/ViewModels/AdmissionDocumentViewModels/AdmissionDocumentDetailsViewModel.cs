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

public partial class AdmissionDocumentDetailsViewModel : ObjectValidationalViewModel
{
    public ObservableCollection<ContractorDto> Contractors { get; set; } = new();

    private readonly IAdmissionDocumentApiService _admissionDocumentApiService;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private AdmissionDocumentDto _admissionDocument = new() { Date = DateOnly.FromDateTime(DateTime.Today) };

    [ObservableProperty]
    private ContractorDto? _selectedContractor;

    public AdmissionDocumentDetailsViewModel(
        IAdmissionDocumentApiService admissionDocumentApiService,
        IDialogService dialogService,
        INavigationService navigationService) : base(dialogService, navigationService)
    {
        _admissionDocumentApiService = admissionDocumentApiService;
        _dialogService = dialogService;
    }

    public async Task LoadDataAsync()
    {
        IsLoading = true;

        Contractors.Clear();

        ContractorDto? selectedContractor = null;

        IEnumerable<ContractorDto> contractors =
            await ApiHelper.SafeApiCallAsync(
                () => _admissionDocumentApiService.GetContractorsAsync(),
                error => _dialogService.ShowMessageAsync("Can't retrive data from server", error))
            ?? new List<ContractorDto>();

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
            SelectedContractor = contractors.Count() > 0 ? Contractors.First() : null;
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
        bool canProceedWithObject = false;

        if (IsEditMode)
        {
            var updateAdmissionDocument = new UpdateAdmissionDocumentDto()
            {
                Date = AdmissionDocument.Date,
                Symbol = AdmissionDocument.Symbol
            };

            canProceedWithObject = ValidationHelper.ValidateObject(
                updateAdmissionDocument,
                ShowValidationErrorsDialogBox);

            if (canProceedWithObject)
            {
                await _admissionDocumentApiService.UpdateAdmissionDocumentAsync(
                    AdmissionDocument.Id, updateAdmissionDocument
                );
            }
        }
        else
        {
            var createAdmissionDocument = new CreateAdmissionDocumentDto()
            {
                Date = AdmissionDocument.Date,
                Symbol = AdmissionDocument.Symbol,
            };

            canProceedWithObject = ValidationHelper.ValidateObject(
                createAdmissionDocument,
                ShowValidationErrorsDialogBox);

            if (canProceedWithObject)
            {
                await _admissionDocumentApiService.CreateAdmissionDocumentAsync(createAdmissionDocument);
            }
        }

        if (canProceedWithObject)
        {
            _navigationService.NavigateTo<AdmissionDocumentsPage>();
        }
    }
}
