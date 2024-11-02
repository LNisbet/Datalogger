using DataLogger.ViewModels;
using System.Windows.Controls;

namespace DataLogger.Views
{
    public partial class CSV_V : Page
    {
        public CSV_V()
        {
            InitializeComponent();
            DataContext = new CSV_VM();
        }
    }
}
