using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Frontend.Views;

public sealed partial class MainPage : Page
{
    private readonly MainPageViewModel _viewModel;

    public MainPage() : this(Ioc.Default.GetRequiredService<MainPageViewModel>())
    {
    }

    public MainPage(MainPageViewModel mainViewModel)
    {
        _viewModel = mainViewModel;

        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        await _viewModel.LoadProperties();

        base.OnNavigatedTo(e);
    }
}
