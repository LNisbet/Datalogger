using DataLogger.ViewModels;
using System.Windows.Controls;

namespace DataLogger.Views
{
    /// <summary>
    /// Interaction logic for CSVView.xaml
    /// </summary>
    public partial class CSVView : Page
    {
        private object? dataContext = null;
        public CSVView()
        {
            InitializeComponent();
            dataContext ??= new CSV_VM();
            DataContext = dataContext;
        }
    }
}
