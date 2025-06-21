using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Frontend.Services;

public interface INavigationService
{
    public void NavigateTo<TPage>(object parameter = null)
        where TPage : Page;
}

public class NavigationService : INavigationService
{
    private readonly MainWindow _mainWindow;

    public NavigationService(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }

    public void NavigateTo<TPage>(object parameter = null)
        where TPage : Page
    {
        _mainWindow.DisplayPage<TPage>(parameter);
    }
}
