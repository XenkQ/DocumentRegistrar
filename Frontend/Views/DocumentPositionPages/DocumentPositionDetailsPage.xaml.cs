using CommunityToolkit.Mvvm.DependencyInjection;
using Dtos.DocumentPositionDtos;
using Frontend.ViewModels.DocumentPositionViewModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Frontend.Views.DocumentPositionPages;

public sealed partial class DocumentPositionDetailsPage : Page
{
    private readonly DocumentPositionDetailsViewModel _documentPositionDetailsViewModel;

    public DocumentPositionDetailsPage() : this(Ioc.Default.GetRequiredService<DocumentPositionDetailsViewModel>())
    {
    }

    public DocumentPositionDetailsPage(DocumentPositionDetailsViewModel documentPositionDetailsViewModel)
    {
        _documentPositionDetailsViewModel = documentPositionDetailsViewModel;

        DataContext = _documentPositionDetailsViewModel;

        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is DocumentPositionDto documentPositionDto)
        {
            _documentPositionDetailsViewModel.DocumentPosition = documentPositionDto;
        }
    }
}
