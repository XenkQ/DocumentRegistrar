using CommunityToolkit.Mvvm.DependencyInjection;
using Dtos.ContractorsDtos;
using Frontend.ViewModels.ContractorViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Frontend.Views.ContractorPages;

public sealed partial class ContractorDetailsPage : Page
{
    private readonly ContractorDetailsViewModel _contractorDetailsViewModel;

    public ContractorDetailsPage() : this(Ioc.Default.GetRequiredService<ContractorDetailsViewModel>())
    {
    }

    public ContractorDetailsPage(ContractorDetailsViewModel contractorsViewModel)
    {
        _contractorDetailsViewModel = contractorsViewModel;

        DataContext = _contractorDetailsViewModel;

        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is ContractorDto contractorDto)
        {
            _contractorDetailsViewModel.Contractor = contractorDto;
        }
    }
}
