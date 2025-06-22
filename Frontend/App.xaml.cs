using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.ViewModels;
using Frontend.ViewModels.ContractorViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;

namespace Frontend;

public sealed partial class App : Application
{
    public new static App Current => (App)Application.Current;

    public static MainWindow? MainWindow { get; private set; }

    public static IConfigurationRoot AppConfiguration { get; private set; }

    public App()
    {
        InitializeComponent();

        SetAppConfiguration();

        ConfigureIocServices();
    }

    private void SetAppConfiguration()
    {
        AppConfiguration = new ConfigurationBuilder()
          .SetBasePath(AppContext.BaseDirectory)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .Build();
    }

    private static void ConfigureIocServices()
    {
        IServiceCollection services = new ServiceCollection()
            .AddSingleton<MainWindow>()
            .AddSingleton<INavigationService, NavigationService>()
            .AddSingleton<MainViewModel>()
            //Contractor
            .AddSingleton<ContractorsViewModel>()
            .AddTransient<ContractorDetailsViewModel>()
            .AddSingleton<IContractorApiService, ContractorApiService>()
            //AdmissionDocument
            .AddSingleton<AdmissionDocumentsViewModel>()
            .AddSingleton<IAdmissionDocumentApiService, AdmissionDocumentApiService>();

        services.AddHttpClient("BackendApi", client =>
        {
            client.BaseAddress = new Uri(AppConfiguration["Backend:BaseUrl"]);
        });

        Ioc.Default.ConfigureServices(services.BuildServiceProvider());
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        MainWindow = Ioc.Default.GetRequiredService<MainWindow>();
        MainWindow?.Activate();
    }
}
