using CommunityToolkit.Mvvm.DependencyInjection;
using Dtos.AdmissionDocumentDtos;
using Frontend.ViewModels.AdmissionDocumentViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;

namespace Frontend.Views.AdmissionDocumentPages;

public sealed partial class AdmissionDocumentDetailsPage : Page
{
    private readonly AdmissionDocumentDetailsViewModel _admissionDocumentDetailsViewModel;

    public AdmissionDocumentDetailsPage() : this(Ioc.Default.GetRequiredService<AdmissionDocumentDetailsViewModel>())
    {
    }

    public AdmissionDocumentDetailsPage(AdmissionDocumentDetailsViewModel admissionDocumentDetailsViewModel)
    {
        _admissionDocumentDetailsViewModel = admissionDocumentDetailsViewModel;

        DataContext = _admissionDocumentDetailsViewModel;

        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);

        if (e.Parameter is AdmissionDocumentDto admissionDocument)
        {
            _admissionDocumentDetailsViewModel.AdmissionDocument = admissionDocument;
        }

        await _admissionDocumentDetailsViewModel.LoadDataAsync();
    }
}
