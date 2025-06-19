using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.ViewModels;
using Frontend.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace Frontend;

public sealed partial class App : Application
{
    public new static App Current => (App)Application.Current;

    public Window MainWindow { get; private set; }

    public App()
    {
        InitializeComponent();

        Ioc.Default.ConfigureServices(new ServiceCollection()
            .AddSingleton<MainViewModel>()
            .AddSingleton<MainPage>()
            .BuildServiceProvider()
        );
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        MainWindow = new MainWindow();
        MainWindow.Activate();
    }
}
