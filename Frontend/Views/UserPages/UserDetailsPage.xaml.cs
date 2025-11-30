using CommunityToolkit.Mvvm.DependencyInjection;
using Dtos.UserDtos;
using Frontend.ViewModels.UserViewModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Frontend.Views.UserPages;

public sealed partial class UserDetailsPage : Page
{
    private readonly UserDetailsViewModel _viewModel;

    public UserDetailsPage() : this(Ioc.Default.GetRequiredService<UserDetailsViewModel>())
    {
    }

    public UserDetailsPage(UserDetailsViewModel userDetailsViewModel)
    {
        _viewModel = userDetailsViewModel;

        DataContext = _viewModel;

        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is UserDto userDto)
        {
            _viewModel.User = userDto;
        }

        await _viewModel.LoadDataAsync();
    }
}
