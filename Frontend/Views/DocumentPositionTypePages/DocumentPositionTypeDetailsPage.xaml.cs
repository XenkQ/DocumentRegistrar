using CommunityToolkit.Mvvm.DependencyInjection;
using Dtos.CreateDocumentTypeDtos;
using Frontend.ViewModels.DocumentPositionTypeViewModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Frontend.Views.DocumentPositionTypePages;

public sealed partial class DocumentPositionTypeDetailsPage : Page
{
    private readonly DocumentPositionTypeDetailsViewModel _viewModel;

    public DocumentPositionTypeDetailsPage() : this(Ioc.Default.GetRequiredService<DocumentPositionTypeDetailsViewModel>())
    {
    }

    public DocumentPositionTypeDetailsPage(DocumentPositionTypeDetailsViewModel documentPositionTypeViewModel)
    {
        _viewModel = documentPositionTypeViewModel;

        DataContext = _viewModel;

        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is DocumentPositionTypeDto documentPositionTypeDto)
        {
            _viewModel.DocumentPositionType = documentPositionTypeDto;
        }
    }
}
