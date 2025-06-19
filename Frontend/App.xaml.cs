using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace Frontend;

internal sealed partial class App : Application
{
    private Window? _window;
    public new static App Current => (App)Application.Current;

    public App()
    {
        InitializeComponent();

        Ioc.Default.ConfigureServices(new ServiceCollection()
            .AddSingleton<MainWindowViewModel>()
            .BuildServiceProvider()
        );
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        _window = new MainWindow();
        _window.Activate();
    }
}
