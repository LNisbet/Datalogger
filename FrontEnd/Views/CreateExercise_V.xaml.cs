using DataLogger.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DataLogger.Views
{
    public partial class CreateExercise_V : Page
    {
        public CreateExercise_V()
        {
            InitializeComponent();
            var VM = new CreateExercise_VM();
            DataContext = VM;
        }
    }
}