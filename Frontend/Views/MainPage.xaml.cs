using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Frontend.Views;

public sealed partial class MainPage : Page
{
    private readonly MainViewModel _mainViewModel;

    public MainPage() : this(Ioc.Default.GetRequiredService<MainViewModel>())
    {
    }

    public MainPage(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;

        InitializeComponent();
    }
}
