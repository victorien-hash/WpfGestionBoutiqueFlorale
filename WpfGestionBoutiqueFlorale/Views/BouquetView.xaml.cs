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
using System.Windows.Shapes;
using WpfGestionBoutiqueFlorale.ViewModels;

namespace WpfGestionBoutiqueFlorale.Views
{
    /// <summary>
    /// Logique d'interaction pour BouquetView.xaml
    /// </summary>
    public partial class BouquetView : Window
    {
        public BouquetView()
        {
            InitializeComponent();
            DataContext = new BouquetViewModel();
        }
    }
}
