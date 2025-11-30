using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.ViewModels.UserViewModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Frontend.Views.UserPages;

public sealed partial class UsersPage : Page
{
    private readonly UsersViewModel _viewModel;

    public UsersPage() : this(Ioc.Default.GetRequiredService<UsersViewModel>())
    {
    }

    public UsersPage(UsersViewModel usersViewModel)
    {
        _viewModel = usersViewModel;

        DataContext = _viewModel;

        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        await _viewModel.LoadDataAsync();
    }
}
