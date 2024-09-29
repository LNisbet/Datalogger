using DataLogger.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DataLogger.Views
{
    public partial class LoggingView : Page
    {
        private Logging_VM VM;
        public LoggingView()
        {
            InitializeComponent();
            TBOX_Date.Visibility = Visibility.Hidden;
            VM = new Logging_VM();
            DataContext = VM;
            UpdateValuesBoxes();
        }

        private void CB_Date_Clicked(object sender, RoutedEventArgs e)
        {
            if (CB_Date.IsChecked == true )
                TBOX_Date.Visibility = Visibility.Visible;
            else
                TBOX_Date.Visibility = Visibility.Hidden;
        }

        private void ExerciseSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateValuesBoxes();
        }
        private void UpdateValuesBoxes()
        {
            if (VM.SelectedExercise == null || VM.SelectedExercise.Unit2 == null)
            {
                Value2_SP.Visibility = Visibility.Collapsed;
                Value2_TB.Text = null;
            }
            else
                Value2_SP.Visibility = Visibility.Visible;

            if (VM.SelectedExercise == null || VM.SelectedExercise.Unit3 == null)
            {
                Value3_SP.Visibility = Visibility.Collapsed;
                Value3_TB.Text = null;
            }
            else
                Value3_SP.Visibility = Visibility.Visible;

            if (VM.SelectedExercise == null || VM.SelectedExercise.Unit4 == null)
            {
                Value4_SP.Visibility = Visibility.Collapsed;
                Value4_TB.Text = null;
            }
            else
                Value4_SP.Visibility = Visibility.Visible;
        }
    }
}