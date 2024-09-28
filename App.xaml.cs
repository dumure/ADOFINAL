﻿using ADOFINAL.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ADOFINAL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var view = new AuthView();

            view.Show();
        }
    }
}
