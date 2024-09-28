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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ADOFINAL.Views
{
    /// <summary>
    /// Interaction logic for MainAdminView.xaml
    /// </summary>
    public partial class MainAdminView : Window
    {
        MainAdminViewModel mainViewModel;
        HistoryAdminViewModel historyViewModel;

        public MainAdminView()
        {
            InitializeComponent();
            mainViewModel = new MainAdminViewModel();
            historyViewModel = new HistoryAdminViewModel();
            DataContext = mainViewModel;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            mainViewModel.LoadProducts(textBox.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            main.Visibility = Visibility.Visible;
            history.Visibility = Visibility.Collapsed;
            mainViewModel.LoadProducts();
            title.Text = "RS ● MAIN";
            textBox.Text = string.Empty;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            history.ItemsSource = null;
            history.ItemsSource = historyViewModel.UserActions.Tables["actions"].DefaultView; 
            history.Visibility = Visibility.Visible;
            main.Visibility = Visibility.Collapsed;
            title.Text = "RS ● HISTORY";
            textBox.Text = string.Empty;
        }
    }
}
