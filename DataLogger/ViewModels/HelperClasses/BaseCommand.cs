using System.Windows.Input;

namespace DataLogger.ViewModels.HelperClasses
{
    public abstract class BaseCommand : ICommand
    {

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public abstract void Execute(object? parameter);
    }
}
