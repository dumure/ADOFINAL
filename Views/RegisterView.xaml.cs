using ADOFINAL.ViewModels;
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
using System.Windows.Shapes;

namespace ADOFINAL.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        RegisterViewModel viewModel;
        public RegisterView()
        {
            InitializeComponent();
            viewModel = new RegisterViewModel();
            viewModel.ButtonEventHandler += (s, a) => Close();
            viewModel.ClearFieldsEventHandler += Button_Click;
            DataContext = viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            viewModel.Password = PasswordBox.Password;
        }

        private void PasswordBox_PasswordChanged_1(object sender, RoutedEventArgs e)
        {
            viewModel.RepeatPassword = RepeatPasswordBox.Password;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            EmailBox.Text = string.Empty;
            UsernameBox.Text = string.Empty;
            PasswordBox.Password = string.Empty;
            RepeatPasswordBox.Password = string.Empty;
        }
    }
}
