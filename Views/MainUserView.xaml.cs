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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ADOFINAL.Views
{
    /// <summary>
    /// Interaction logic for MainUserView.xaml
    /// </summary>
    public partial class MainUserView : Window
    {
        MainUserViewModel mainViewModel;
        CartUserViewModel cartViewModel;
        HistoryUserViewModel historyViewModel;
        public MainUserView(User user)
        {
            InitializeComponent();
            mainViewModel = new MainUserViewModel(user);
            cartViewModel = new CartUserViewModel(user);
            historyViewModel = new HistoryUserViewModel(user);
            DataContext = mainViewModel;
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cart.Visibility == Visibility.Visible)
            {
                cartViewModel.LoadProducts(textBox.Text);
            }
            else if (main.Visibility == Visibility.Visible)
            {
                mainViewModel.LoadProducts(textBox.Text);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cart.Visibility = Visibility.Visible;
            main.Visibility = Visibility.Collapsed;
            history.Visibility = Visibility.Collapsed;
            DataContext = cartViewModel;
            cartViewModel.LoadProducts();
            title.Text = "RS ● CART";
            textBox.Text = string.Empty;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            history.ItemsSource = null;
            history.ItemsSource = historyViewModel.UserActions.Tables["actions"].DefaultView;
            history.Visibility = Visibility.Visible;
            cart.Visibility = Visibility.Collapsed;
            main.Visibility = Visibility.Collapsed;
            DataContext = historyViewModel;
            title.Text = "RS ● HISTORY";
            textBox.Text = string.Empty;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            main.Visibility = Visibility.Visible;
            history.Visibility = Visibility.Collapsed;
            cart.Visibility = Visibility.Collapsed;
            DataContext = mainViewModel;
            mainViewModel.LoadProducts();
            title.Text = "RS ● MAIN";

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (cart.Visibility == Visibility.Visible)
            {
                var view = new AccountUserView(cartViewModel.CurrentUser);
                view.ShowDialog();
                cartViewModel.RefreshUser();
            }
            else if (main.Visibility == Visibility.Visible)
            {
                var view = new AccountUserView(mainViewModel.CurrentUser);
                view.ShowDialog();
                mainViewModel.RefreshUser();
            }
            
        }
    }
}
