using CommunityToolkit.Mvvm.DependencyInjection;
using Dtos.DocumentPositionDtos;
using Frontend.ControlsConfigurators;
using Frontend.ViewModels.DocumentPositionViewModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Frontend.Views.DocumentPositionPages;

public sealed partial class DocumentPositionDetailsPage : Page
{
    private readonly DocumentPositionDetailsViewModel _viewModel;

    public DocumentPositionDetailsPage() : this(Ioc.Default.GetRequiredService<DocumentPositionDetailsViewModel>())
    {
    }

    public DocumentPositionDetailsPage(DocumentPositionDetailsViewModel documentPositionDetailsViewModel)
    {
        _viewModel = documentPositionDetailsViewModel;

        DataContext = _viewModel;

        InitializeComponent();

        var numberBoxConfigurator = new NumberBoxConfigurator<NumberBox>();
        numberBoxConfigurator.ConfigureControl(UnitPriceNumberBox);
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is DocumentPositionDto documentPositionDto)
        {
            _viewModel.DocumentPosition = documentPositionDto;
        }

        await _viewModel.LoadDataAsync();
    }
}
