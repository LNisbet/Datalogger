using DataLogger.ViewModels;
using System.Windows.Navigation;

namespace DataLogger.Views
{
    /// <summary>
    /// Interaction logic for MainWindow_V.xaml
    /// </summary>
    public partial class MainWindow_V : NavigationWindow
    {
        public MainWindow_V()
        {
            InitializeComponent();
            var VM = new MainWindow_VM();
            DataContext = VM;
        }
    }
}
