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
    /// <summary>
    /// Interaction logic for Graphs_V.xaml
    /// </summary>
    public partial class Charting_V : Page
    {
        private object? dataContext = null;
        public Charting_V()
        {
            InitializeComponent();
            dataContext ??= new Charting_VM();
            DataContext = dataContext;
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            var viewModel = DataContext as Charting_VM;

            // Clear and update NewExerciseTags based on selected items
            viewModel.SelectedExercises.Clear();
            foreach (string selectedItem in listBox.SelectedItems)
            {
                viewModel.SelectedExercises.Add(selectedItem);
            }
            viewModel.OnPropertyChanged(nameof(viewModel.SelectedExercises));
        }
    }
}
