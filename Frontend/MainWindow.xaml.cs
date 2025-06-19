using Frontend.Views;
using Microsoft.UI.Xaml;

namespace Frontend;

internal sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Body.Navigate(typeof(MainPage));
    }

    public void SetBody(UIElement uiElement) => Body.Content = uiElement;

    public UIElement GetBody() => Body;
}
