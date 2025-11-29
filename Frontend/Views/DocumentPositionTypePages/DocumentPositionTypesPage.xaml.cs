using CommunityToolkit.Mvvm.DependencyInjection;
using Dtos.CreateDocumentTypeDtos;
using Frontend.ViewModels.DocumentPositionTypeViewModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Frontend.Views.DocumentPositionTypePages;

public sealed partial class DocumentPositionTypesPage : Page
{
    private readonly DocumentPositionTypesViewModel _viewModel;

    public DocumentPositionTypesPage() : this(Ioc.Default.GetRequiredService<DocumentPositionTypesViewModel>())
    {
    }

    public DocumentPositionTypesPage(DocumentPositionTypesViewModel documentPositionTypeViewModel)
    {
        _viewModel = documentPositionTypeViewModel;

        DataContext = _viewModel;

        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        await _viewModel.LoadDataAsync();
    }
}
