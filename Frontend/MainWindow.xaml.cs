using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Frontend;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public Page? DisplayedPage => Body.Content as Page;

    public void DisplayPage<TPage>(object parameter = null)
        where TPage : Page
    {
        if (parameter is null)
        {
            Body.Navigate(typeof(TPage));
        }
        else
        {
            Body.Navigate(typeof(TPage), parameter);
        }
    }
}
