using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Frontend.Views;

public sealed partial class MainPage : Page
{
    private readonly MainPageViewModel _mainViewModel;

    public MainPage() : this(Ioc.Default.GetRequiredService<MainPageViewModel>())
    {
    }

    public MainPage(MainPageViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;

        InitializeComponent();
    }
}
