using CommunityToolkit.Mvvm.ComponentModel;
using Frontend.Services;

namespace Frontend.ViewModels
{
    public abstract partial class ViewModelBase : ObservableObject
    {
        protected readonly INavigationService _navigationService;

        [ObservableProperty]
        private bool _isLoading;

        public ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
