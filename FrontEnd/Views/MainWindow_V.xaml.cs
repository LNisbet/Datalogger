﻿using DataLogger.ViewModels;
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
