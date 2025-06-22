using CommunityToolkit.Mvvm.DependencyInjection;
using Dtos.AdmissionDocumentDtos;
using Frontend.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Frontend.Views.DocumentPositionPages;

public sealed partial class DocumentPositionsPage : Page
{
    private readonly DocumentPositionsViewModel _documentPositionsViewModel;

    public DocumentPositionsPage() : this(Ioc.Default.GetRequiredService<DocumentPositionsViewModel>())
    {
    }

    public DocumentPositionsPage(DocumentPositionsViewModel documentPositionsViewModel)
    {
        _documentPositionsViewModel = documentPositionsViewModel;

        DataContext = _documentPositionsViewModel;

        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is AdmissionDocumentDto admissionDocument)
        {
            _documentPositionsViewModel.AdmissionDocument = admissionDocument;
            await _documentPositionsViewModel.LoadDataAsync(admissionDocument.Id);
        }
    }
}
