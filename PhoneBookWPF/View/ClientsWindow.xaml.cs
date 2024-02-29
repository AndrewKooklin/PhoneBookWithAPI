﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PhoneBookWPF.ViewModel;

namespace PhoneBookWPF.View
{
    /// <summary>
    /// Interaction logic for ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : Window
    {
        //StoreWithEFDBEntities _context;

        public ClientsWindow()
        {
            InitializeComponent();
            DataContext = new ClientsWindowViewModel();
        }

        private void ClientsWindow_Load(object sender, RoutedEventArgs e)
        {
        }
    }
}