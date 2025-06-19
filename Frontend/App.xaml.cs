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

    public Window? MainWindow { get; private set; }

    public App()
    {
        InitializeComponent();

        Ioc.Default.ConfigureServices(new ServiceCollection()
            .AddSingleton<MainWindow>()
            .AddSingleton<MainPage>()
            .AddSingleton<INavigationService, NavigationService>()
            .AddSingleton<MainViewModel>()
            .AddSingleton<ContractorsViewModel>()
            .AddSingleton<ContractorsPage>()
            .AddSingleton<AdmissionDocumentsViewModel>()
            .AddSingleton<AdmissionDocumentsPage>()
            .BuildServiceProvider()
        );
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        PrepareMainWindow();

        NavigateToMainPage();
    }

    private void PrepareMainWindow()
    {
        MainWindow = Ioc.Default.GetRequiredService<MainWindow>();
        MainWindow?.Activate();
    }

    private void NavigateToMainPage()
    {
        INavigationService navigationService = Ioc.Default.GetRequiredService<INavigationService>();
        navigationService.NavigateToPageByName(nameof(MainPage));
    }
}
