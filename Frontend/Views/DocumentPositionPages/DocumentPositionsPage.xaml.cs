using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.ViewModels;
using Microsoft.UI.Xaml.Controls;

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
}
