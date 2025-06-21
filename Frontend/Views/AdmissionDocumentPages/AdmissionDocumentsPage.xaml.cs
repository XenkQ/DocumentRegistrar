using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.ViewModels;
using Microsoft.UI.Xaml.Controls;

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

        InitializeComponent();
    }
}
