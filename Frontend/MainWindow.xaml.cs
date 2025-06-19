using Frontend.Views;
using Microsoft.UI.Xaml;

namespace Frontend;

internal sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void SetBody(UIElement uiElement) => Body.Content = uiElement;

    public UIElement GetBody() => Body;
}
