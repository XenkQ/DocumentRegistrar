using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Frontend.Views;

public sealed partial class AdmissionDocumentsPage : Page
{
    private readonly AdmissionDocumentsViewModel _admissionDocumentsViewModel;

    public AdmissionDocumentsPage() : this(Ioc.Default.GetRequiredService<AdmissionDocumentsViewModel>())
    {
    }

    public AdmissionDocumentsPage(AdmissionDocumentsViewModel admissionDocumentsViewModel)
    {
        _admissionDocumentsViewModel = admissionDocumentsViewModel;

        DataContext = _admissionDocumentsViewModel;

        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        await _admissionDocumentsViewModel.LoadDataAsync();
    }
}
