using CommunityToolkit.Mvvm.DependencyInjection;
using Frontend.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Frontend.Views;

public sealed partial class ContractorsPage : Page
{
    private readonly ContractorsViewModel _contractorsViewModel;

    public ContractorsPage() : this(Ioc.Default.GetRequiredService<ContractorsViewModel>())
    {
    }

    public ContractorsPage(ContractorsViewModel contractorsViewModel)
    {
        _contractorsViewModel = contractorsViewModel;

        InitializeComponent();
    }
}
