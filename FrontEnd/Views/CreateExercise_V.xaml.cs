using DataLogger.ViewModels;
using System.Windows.Controls;
using SQLight_Database;

namespace DataLogger.Views
{
    public partial class CreateExercise_V : Page
    {
        public CreateExercise_V()
        {
            InitializeComponent();
            var VM = new CreateExercise_VM();
            DataContext = VM;
            ComboBox_Unit1.ItemsSource = ComboBox_Unit2.ItemsSource = ComboBox_Unit3.ItemsSource = Enum.GetValues(typeof(Enums.Units)).Cast<Enums.Units>();
        }
    }
}