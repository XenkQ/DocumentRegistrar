using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Frontend.Views;

public sealed partial class LoginPage : Page
{
    private readonly LoginViewModel _viewModel;

    public LoginPage() : this(Ioc.Default.GetRequiredService<LoginViewModel>())
    {
    }

    public LoginPage(LoginViewModel loginViewModel)
    {
        _viewModel = loginViewModel;

        DataContext = _viewModel;

        InitializeComponent();
    }
}
