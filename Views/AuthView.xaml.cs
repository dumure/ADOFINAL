﻿using ADOFINAL.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADOFINAL.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AuthView : Window
    {
        AuthViewModel viewModel;
        public AuthView()
        {
            InitializeComponent();
            viewModel = new AuthViewModel();
            viewModel.ButtonEventHandler += (s, a) => { Close(); };
            viewModel.ClearFieldsEventHandler += Button_Click;
            DataContext = viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            viewModel.Password = PasswordBox.Password;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            UsernameBox.Text = string.Empty;
            PasswordBox.Password = string.Empty;
        }
    }
}