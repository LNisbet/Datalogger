using DataLogger.ViewModels;
using System.Windows;

namespace DataLogger.Views
{
    public partial class MainWindow_V : Window
    {
        public MainWindow_V()
        {
            InitializeComponent();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Retrieve the ViewModel and trigger the ClosingCommand
            if (DataContext is MainWindow_VM viewModel && viewModel.CloseApp.CanExecute(null))
            {
                viewModel.CloseApp.Execute(null);
            }
        }
    }
}
