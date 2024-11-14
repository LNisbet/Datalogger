using DataLogger.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DataLogger.Views
{
    public partial class Logging_V : UserControl
    {
        public Logging_V()
        {
            InitializeComponent();
            TBOX_Date.Visibility = Visibility.Hidden;
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