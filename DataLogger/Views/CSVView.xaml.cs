using DataLogger.ViewModels;
using System.Windows.Controls;

namespace DataLogger.Views
{
    /// <summary>
    /// Interaction logic for CSVView.xaml
    /// </summary>
    public partial class CSVView : Page
    {
        public CSVView()
        {
            InitializeComponent();
            var VM = new CSV_VM();
            DataContext = VM;
        }
    }
}
