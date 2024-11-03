using System.Windows.Controls;
using DataLogger.ViewModels;

namespace DataLogger.Views
{
    public partial class CreateExercise_V : UserControl
    {
        private int unitsDisplayed;
        private int UnitsDisplayed 
        { 
            get => unitsDisplayed;
            set
            {
                unitsDisplayed = value;
                UpdateUnitsBoxes();
            }
        }
        public CreateExercise_V()
        {
            InitializeComponent();
            DataContext = new CreateExercise_VM();
            UnitsDisplayed = 1;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ListBox listBox || DataContext is not CreateExercise_VM viewModel)
                return;

            // Clear and update NewExerciseTags based on selected items
            viewModel.NewExerciseTags.Clear();
            foreach (string selectedItem in listBox.SelectedItems)
            {
                viewModel.NewExerciseTags.Add(selectedItem);
            }
        }

        private void UpdateUnitsBoxes()
        {
            switch (UnitsDisplayed)
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
                    UnitsDisplayed = 1;
                    break;
            }
        }

        private void MoreUnits(object sender, System.Windows.RoutedEventArgs e)
        {
            UnitsDisplayed++;
        }

        private void LessUnits(object sender, System.Windows.RoutedEventArgs e)
        {
            UnitsDisplayed--;
        }
    }
}
