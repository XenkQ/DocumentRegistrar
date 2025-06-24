using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace Frontend.Services;

public interface IDialogService
{
    public Task ShowMessageAsync(string title, string message);
}

public class DialogService : IDialogService
{
    public async Task ShowMessageAsync(string title, string message)
    {
        XamlRoot? xamlRoot = App.Current.MainWindow.DisplayedPage.XamlRoot;

        if (xamlRoot is null)
        {
            throw new InvalidOperationException("There is no page displayed on which dialog can be shown");
        }

        var dialog = new ContentDialog
        {
            Title = title,
            Content = message,
            CloseButtonText = "OK",
            XamlRoot = xamlRoot,
        };

        await dialog.ShowAsync();
    }
}
