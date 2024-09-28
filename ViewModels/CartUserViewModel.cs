using ADOFINAL.Commands;
using ADOFINAL.Database;
using ADOFINAL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ADOFINAL.ViewModels
{
    public class CartUserViewModel : ViewModel
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
        public CartUserViewModel(User user)
        {
            CurrentUser = user;
            Products = new ObservableCollection<Product>();
            LoadProducts();
        }
        public void LoadProducts(string search = "")
        {
            Products.Clear();
            database = new ShopDatabase();
            var products = database.GetProducts(search);
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Owner == CurrentUser.Username)
                {
                    var product = products[i];
                    product.BuyCommand = new DelegateCommand(() => { BuyProduct(product); });
                    product.DeleteCommand = new DelegateCommand(() => { RemoveFromCart(product); });
                    Products.Add(product);
                }
            }
        }
        public void BuyProduct(Product product)
        {
            if (MessageBox.Show("Do you want to buy this product?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (CurrentUser.Cash >= product.Price)
                {
                    CurrentUser.Cash -= product.Price;
                    database.UpdateUser(CurrentUser);
                    database.AddUserAction(CurrentUser, product, 1);
                    LoadProducts();
                }
                else
                {
                    MessageBox.Show("Not enough money on balance");
                }
            }
            RefreshUser();
        }

        public void RemoveFromCart(Product product)
        {
            database.AddUserAction(CurrentUser, product, 2);
            LoadProducts();
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
