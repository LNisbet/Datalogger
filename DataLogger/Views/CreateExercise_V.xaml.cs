using System.Windows.Controls;
using DataLogger.ViewModels;

namespace DataLogger.Views
{
    public partial class CreateExercise_V : Page
    {
        private object? dataContext = null;
        private int unitsDisplayed;
        public CreateExercise_V()
        {
            InitializeComponent();
            dataContext ??= new CreateExercise_VM();
            DataContext = dataContext;
            unitsDisplayed = 1;
            UpdateUnitsBoxes();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            var viewModel = DataContext as CreateExercise_VM;

            // Clear and update NewExerciseTags based on selected items
            viewModel.NewExerciseTags.Clear();
            foreach (string selectedItem in listBox.SelectedItems)
            {
                viewModel.NewExerciseTags.Add(selectedItem);
            }
        }

        private void UpdateUnitsBoxes()
        {
            switch (unitsDisplayed)
            {
                case 1:
                    LessUnits_Button.IsEnabled = false;
                    Unit2_SP.Visibility = System.Windows.Visibility.Collapsed;
                    Unit2_CB.Text = null;
                    Unit3_SP.Visibility = System.Windows.Visibility.Collapsed;
                    Unit3_CB.Text = null;
                    Unit4_SP.Visibility = System.Windows.Visibility.Collapsed;
                    Unit4_CB.Text = null;
                    MoreUnits_Button.IsEnabled = true;
                    break;
                case 2:
                    LessUnits_Button.IsEnabled = true;
                    Unit2_SP.Visibility = System.Windows.Visibility.Visible;
                    Unit3_SP.Visibility = System.Windows.Visibility.Collapsed;
                    Unit3_CB.Text = null;
                    Unit4_SP.Visibility = System.Windows.Visibility.Collapsed;
                    Unit4_CB.Text = null;
                    MoreUnits_Button.IsEnabled = true;
                    break;
                case 3:
                    LessUnits_Button.IsEnabled = true;
                    Unit2_SP.Visibility = System.Windows.Visibility.Visible;
                    Unit3_SP.Visibility = System.Windows.Visibility.Visible;
                    Unit4_SP.Visibility = System.Windows.Visibility.Collapsed;
                    Unit4_CB.Text = null;
                    MoreUnits_Button.IsEnabled = true;
                    break;
                case 4:
                    LessUnits_Button.IsEnabled = true;
                    Unit2_SP.Visibility = System.Windows.Visibility.Visible;
                    Unit3_SP.Visibility = System.Windows.Visibility.Visible;
                    Unit4_SP.Visibility = System.Windows.Visibility.Visible;
                    MoreUnits_Button.IsEnabled = false;
                    break;
                default:
                    unitsDisplayed = 1;
                    break;
            }
        }

        private void MoreUnits(object sender, System.Windows.RoutedEventArgs e)
        {
            unitsDisplayed++;
            UpdateUnitsBoxes();
        }

        private void LessUnits(object sender, System.Windows.RoutedEventArgs e)
        {
            unitsDisplayed--;
            UpdateUnitsBoxes();
        }
    }
}
