﻿using PhoneBookWPF.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PhoneBookWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ClientsWindow clientsWindow = new ClientsWindow();
        public static ProductsWindow productsWindow = new ProductsWindow();
        public static ProductsClientWindow productsClientWindow = new ProductsClientWindow();
    }
}