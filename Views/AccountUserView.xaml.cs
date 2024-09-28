using ADOFINAL.Image;
using ADOFINAL.Models;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ADOFINAL.Database;
using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Design;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;


namespace ADOFINAL.Views
{
    /// <summary>
    /// Interaction logic for AccountUserView.xaml
    /// </summary>
    public partial class AccountUserView : Window
    {
        AccountUserViewModel viewModel;
        public AccountUserView(User user)
        {
            InitializeComponent();
            viewModel = new AccountUserViewModel(user);
            password.Password = "          ";
            DataContext = viewModel;
            viewModel.SaveButton += (s,a) => Close();
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            viewModel.Password = password.Password;
        }
    }
}
