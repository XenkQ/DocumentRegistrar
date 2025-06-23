using CommunityToolkit.Mvvm.DependencyInjection;
using Dtos.AdmissionDocumentDtos;
using Frontend.ViewModels.AdmissionDocumentViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Frontend.Views.AdmissionDocumentPages;

public sealed partial class AdmissionDocumentDetailsPage : Page
{
    private readonly AdmissionDocumentDetailsViewModel _viewModel;

    public AdmissionDocumentDetailsPage() : this(Ioc.Default.GetRequiredService<AdmissionDocumentDetailsViewModel>())
    {
    }

    public AdmissionDocumentDetailsPage(AdmissionDocumentDetailsViewModel admissionDocumentDetailsViewModel)
    {
        _viewModel = admissionDocumentDetailsViewModel;

        DataContext = _viewModel;

        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);

        if (e.Parameter is AdmissionDocumentDto admissionDocument)
        {
            _viewModel.AdmissionDocument = admissionDocument;
        }

        await _viewModel.LoadDataAsync();
    }
}
