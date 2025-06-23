using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Frontend.Views;

public sealed partial class AdmissionDocumentsPage : Page
{
    private readonly AdmissionDocumentsViewModel _viewModel;

    public AdmissionDocumentsPage() : this(Ioc.Default.GetRequiredService<AdmissionDocumentsViewModel>())
    {
    }

    public AdmissionDocumentsPage(AdmissionDocumentsViewModel admissionDocumentsViewModel)
    {
        _viewModel = admissionDocumentsViewModel;

        DataContext = _viewModel;

        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        await _viewModel.LoadDataAsync();
    }
}
