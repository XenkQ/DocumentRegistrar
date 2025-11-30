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

    public bool IsEditMode => AdmissionDocument.Id != default;

    public async Task LoadDataAsync()
    {
        IsLoading = true;

        Contractors.Clear();

        ContractorDto? selectedContractor = null;

        IEnumerable<ContractorDto> contractors =
            await ApiHelper.SafeApiCallAsync(
                () => _admissionDocumentApiService.GetContractorsAsync(),
                (error, message) => _dialogService.ShowErrorMessage(message, error))
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
                Symbol = AdmissionDocument.Symbol,
                ContractorId = SelectedContractor?.Id ?? AdmissionDocument.ContractorId
            };

            canProceedWithObject = ValidationHelper.ValidateObject(
                updateAdmissionDocument,
                ShowValidationErrorsDialogBox);

            if (canProceedWithObject)
            {
                await ApiHelper.SafeApiCallAsync(
                    () => _admissionDocumentApiService.UpdateAdmissionDocumentAsync(AdmissionDocument.Id, updateAdmissionDocument),
                    (error, _) => _dialogService.ShowErrorMessage("Can't update admission document", error)
                );
            }
        }
        else
        {
            var createAdmissionDocument = new CreateAdmissionDocumentDto()
            {
                Date = AdmissionDocument.Date,
                Symbol = AdmissionDocument.Symbol,
                ContractorId = SelectedContractor?.Id ?? AdmissionDocument.ContractorId
            };

            canProceedWithObject = ValidationHelper.ValidateObject(
                createAdmissionDocument,
                ShowValidationErrorsDialogBox);

            if (canProceedWithObject)
            {
                await ApiHelper.SafeApiCallAsync(
                    () => _admissionDocumentApiService.CreateAdmissionDocumentAsync(createAdmissionDocument),
                    (error, _) => _dialogService.ShowErrorMessage("Can't create admission document", error)
                );
            }
        }

        if (canProceedWithObject)
        {
            _navigationService.NavigateTo<AdmissionDocumentsPage>();
        }
    }
}
