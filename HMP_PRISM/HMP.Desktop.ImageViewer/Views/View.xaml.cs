﻿using System;
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

namespace HMP.Desktop.ImageViewer.Views
{
    /// <summary>
    /// Logique d'interaction pour View.xaml
    /// </summary>
    public partial class View : UserControl
    {
        public View()
        {
            InitializeComponent();
            ViewModels.ViewModel vm = new ViewModels.ViewModel();

            this.DataContext = vm;
        }
    }
}
