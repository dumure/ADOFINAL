using ADOFINAL.Commands;
using ADOFINAL.Database;
using ADOFINAL.Views;
using ADOFINAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ADOFINAL.Encoder;

namespace ADOFINAL.ViewModels
{
    public class AuthViewModel
    {
        public EventHandler ButtonEventHandler { get; set; }
        public EventHandler ClearFieldsEventHandler { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICommand SignInCommand { get; }
        public ICommand SignUpCommand { get; }
        public AuthViewModel()
        {
            SignInCommand = new DelegateCommand(OnSignIn);
            SignUpCommand = new DelegateCommand(OnSignUp);
        }
        public void OnSignIn()
        {
            if (Username == "admin" && Password == "admin")
            {
                var mainAdminView = new MainAdminView();
                mainAdminView.Show();
                ButtonEventHandler?.Invoke(this, new EventArgs());
                MessageBox.Show("Successful authorization. Welcome!");
            }
            else if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                Password = Password.Replace(" ", "");
                Username = Username.Replace(" ", "");
                var shopDB = new ShopDatabase();
                var users = shopDB.GetUsers(string.Empty);
                User required_user = new User();
                bool flag = true;
                foreach (var user in users)
                {
                    if (user.Username.ToLower() == Username.ToLower())
                    {
                        required_user = user;
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    MessageBox.Show("There is no user with this username.");
                }
                else
                {
                    if (EncoderSHA256.ToSHA256(Password) == required_user.Password)
                    {
                        var mainUserView = new MainUserView(required_user);
                        mainUserView.Show();
                        ButtonEventHandler?.Invoke(this, new EventArgs());
                        MessageBox.Show("Successful authorization. Welcome!");
                    }
                    else
                    {
                        MessageBox.Show("Incorrect password.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Fill all boxes.");
            }
            ClearFieldsEventHandler?.Invoke(this, new EventArgs());
            Username = string.Empty;
            Username = string.Empty;
        }
        public void OnSignUp()
        {
            var registerView = new RegisterView();
            registerView.Show();
            ButtonEventHandler?.Invoke(this, new EventArgs());
        }
    }
}
