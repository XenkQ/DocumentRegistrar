using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Frontend.Views;

public sealed partial class ContractorsPage : Page
{
    private readonly ContractorsViewModel _viewModel;

    public ContractorsPage() : this(Ioc.Default.GetRequiredService<ContractorsViewModel>())
    {
    }

    public ContractorsPage(ContractorsViewModel contractorsViewModel)
    {
        _viewModel = contractorsViewModel;

        DataContext = _viewModel;

        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        await _viewModel.LoadDataAsync();
    }
}
