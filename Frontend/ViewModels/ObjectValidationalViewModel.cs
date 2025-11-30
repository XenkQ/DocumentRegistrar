using Frontend.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Frontend.ViewModels
{
    public abstract class ObjectValidationalViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;

        public ObjectValidationalViewModel(
            IDialogService dialogService,
            INavigationService navigationService) : base(navigationService)
        {
            _dialogService = dialogService;
        }

        protected void ShowValidationErrorsDialogBox(IEnumerable<ValidationResult> validationResults)
        {
            _dialogService.ShowErrorMessage(
                "Validation Error",
                string.Join('\n', validationResults.ToList().Select(vr => vr.ErrorMessage)
                ));
        }
    }
}
