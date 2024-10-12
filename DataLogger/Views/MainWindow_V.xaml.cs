using DataLogger.ViewModels;
using System.Windows.Navigation;

namespace DataLogger.Views
{
    /// <summary>
    /// Interaction logic for MainWindow_V.xaml
    /// </summary>
    public partial class MainWindow_V : NavigationWindow
    {
        private object? dataContext = null;
        public MainWindow_V()
        {
            InitializeComponent();
            dataContext ??= new MainWindow_VM();
            DataContext = dataContext;
        }
    }
}
