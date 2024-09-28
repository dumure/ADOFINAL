using ADOFINAL.Commands;
using ADOFINAL.Models;
using ADOFINAL.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ADOFINAL.ViewModels
{
    public class MainUserViewModel : ViewModel
    {
        public ObservableCollection<Product> Products { get; set; }
        private ShopDatabase database;
        private User _currentUser;
        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                if (!_currentUser?.Equals(value) ?? value != null)
                {
                    _currentUser = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(CurrentUser)));
                }
            }
        }
        public MainUserViewModel(User user)
        {
            Products = new ObservableCollection<Product>();
            CurrentUser = user;
            LoadProducts();
        }
        public void LoadProducts(string search = "")
        {
            Products.Clear();
            database = new ShopDatabase();
            var products = database.GetProducts(search);
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Owner == "admin")
                {
                    var product = products[i];
                    product.BuyCommand = new DelegateCommand(() => { AddToCart(product); });
                    Products.Add(product);
                }
            }
        }
        public void AddToCart(Product product)
        {
            if (MessageBox.Show($"Do you want to add {product.Name} in your cart?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                database.AddUserAction(CurrentUser, product, 0);
                LoadProducts();
            }
        }

        public void RefreshUser()
        {
            database = new ShopDatabase();
            var users = database.GetUsers(string.Empty);
            foreach (var user in users)
            {
                if (user.Username == CurrentUser.Username)
                {
                    CurrentUser = user;
                }
            }
        }
    }
}
