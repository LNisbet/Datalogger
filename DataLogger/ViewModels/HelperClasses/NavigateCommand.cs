using System.Windows.Input;

namespace DataLogger.ViewModels.HelperClasses
{
    /*public class NavigateCommand<TViewModel> : BaseCommand
        where TViewModel : Base_VM
    {
        private readonly Predicate<object> _canExecute;
        private readonly NavigationService<TViewModel> _navigationService;

        public NavigateCommand(NavigationService<TViewModel> navigationService, Predicate<object> canExecute)
        {
            _navigationService = navigationService;
            _canExecute = canExecute;
        }

        public NavigateCommand(NavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
            _canExecute = param => true;
        }

        public override bool CanExecute(object? parameter)
        {
            if (parameter == null)
                return false;

            return _canExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _navigationService.NavigateTo();
        }
    }*/
}
