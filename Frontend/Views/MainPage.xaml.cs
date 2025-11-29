using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.ViewModels;
using Microsoft.UI.Xaml.Controls;

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
}
