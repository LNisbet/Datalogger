using DataLogger.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataLogger.Views
{
    public partial class Charting_V : UserControl
    {
        public Charting_V()
        {
            InitializeComponent();
            DataContext = new Charting_VM();
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ListBox listBox || DataContext is not Charting_VM viewModel)
                return;

            foreach (var listItem in viewModel.ExerciseList)
            {
                if (listBox.SelectedItems.Contains(listItem) && !viewModel.SelectedExercises.Contains(listItem))
                {
                    viewModel.SelectedExercises.Add(listItem);
                    break;
                }
                if (!listBox.SelectedItems.Contains(listItem) && viewModel.SelectedExercises.Contains(listItem))
                {
                    viewModel.SelectedExercises.Remove(listItem);
                    break;
                }
            }
            viewModel.OnPropertyChanged(nameof(viewModel.SelectedExercises));
        }
    }
}
