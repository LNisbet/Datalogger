using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataLogger.ViewModels;
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
