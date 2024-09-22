using DataLogger.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DataLogger.Views
{
    public partial class LoggingView : Page
    {
        public LoggingView()
        {
            InitializeComponent();
            TBOX_Date.Visibility = Visibility.Hidden;
            var VM = new Logging_VM();
            DataContext = VM;
        }

        private void CB_Date_Clicked(object sender, RoutedEventArgs e)
        {
            if (CB_Date.IsChecked == true )
                TBOX_Date.Visibility = Visibility.Visible;
            else
                TBOX_Date.Visibility = Visibility.Hidden;
        }
    }
}