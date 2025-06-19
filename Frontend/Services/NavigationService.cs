using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.Views;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Frontend.Services;

internal interface INavigationService
{
    public void NavigateToPageByName(string name);
}

internal class NavigationService : INavigationService
{
    private readonly MainWindow _bodyContainer;

    public NavigationService(MainWindow bodyContainer)
    {
        _bodyContainer = bodyContainer;
    }

    public void NavigateToPageByName(string name)
    {
        Page page = name switch
        {
            nameof(MainPage) => Ioc.Default.GetRequiredService<MainPage>(),
            nameof(ContractorsPage) => Ioc.Default.GetRequiredService<ContractorsPage>(),
            nameof(AdmissionDocumentsPage) => Ioc.Default.GetRequiredService<AdmissionDocumentsPage>(),
            _ => throw new ArgumentException($"Page with name {name} does not exist.", nameof(name)),
        };

        _bodyContainer.SetBody(page);
    }
}
