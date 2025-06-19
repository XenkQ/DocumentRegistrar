using Frontend.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Frontend.Views;

internal sealed partial class MainPage : Page
{
    private readonly MainViewModel _mainViewModel;

    public MainPage(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;

        InitializeComponent();
    }
}
