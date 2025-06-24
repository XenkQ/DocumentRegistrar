using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.Services;
using Frontend.Services.Api;
using Frontend.ViewModels;
using Frontend.ViewModels.AdmissionDocumentViewModels;
using Frontend.ViewModels.ContractorViewModels;
using Frontend.ViewModels.DocumentPositionViewModel;
using Frontend.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;

namespace Frontend;

public sealed partial class App : Application
{
    public new static App Current => (App)Application.Current;

    public MainWindow? MainWindow { get; private set; }

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
            .AddSingleton<IDialogService, DialogService>()
            .AddSingleton<MainViewModel>()
            //Contractor
            .AddSingleton<ContractorsViewModel>()
            .AddTransient<ContractorDetailsViewModel>()
            .AddSingleton<IContractorApiService, ContractorApiService>()
            //AdmissionDocument
            .AddSingleton<AdmissionDocumentsViewModel>()
            .AddTransient<AdmissionDocumentDetailsViewModel>()
            .AddSingleton<IAdmissionDocumentApiService, AdmissionDocumentApiService>()
            //DocumentPosition
            .AddSingleton<DocumentPositionsViewModel>()
            .AddTransient<DocumentPositionDetailsViewModel>()
            .AddSingleton<IDocumentPositionApiService, DocumentPositionApiService>();

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
        MainWindow?.DisplayPage<MainPage>();
    }
}
