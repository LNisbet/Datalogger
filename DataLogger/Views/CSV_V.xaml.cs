using DataLogger.ViewModels;
using System.Windows.Controls;

namespace DataLogger.Views
{
    public partial class CSV_V : Page
    {
        private object? dataContext = null;
        public CSV_V()
        {
            InitializeComponent();
            dataContext ??= new CSV_VM();
            DataContext = dataContext;
        }
    }
}
