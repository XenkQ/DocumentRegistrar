using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.Services;
using Frontend.ViewModels;
using Frontend.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace Frontend;

public sealed partial class App : Application
{
    public new static App Current => (App)Application.Current;

    public MainWindow? MainWindow { get; private set; }

    public App()
    {
        InitializeComponent();

        Ioc.Default.ConfigureServices(new ServiceCollection()
            .AddSingleton<MainWindow>()
            .AddSingleton<INavigationService, NavigationService>()
            .AddSingleton<MainViewModel>()
            .AddSingleton<ContractorsViewModel>()
            .AddSingleton<AdmissionDocumentsViewModel>()
            .BuildServiceProvider()
        );
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        PrepareMainWindow();
    }

    private void PrepareMainWindow()
    {
        MainWindow = Ioc.Default.GetRequiredService<MainWindow>();
        MainWindow?.Activate();
    }
}
