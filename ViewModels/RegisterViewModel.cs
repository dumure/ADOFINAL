using ADOFINAL.Commands;
using ADOFINAL.Views;
using ADOFINAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using ADOFINAL.Database;
using ADOFINAL.Encoder;

namespace ADOFINAL.ViewModels
{
    public class RegisterViewModel
    {
        public EventHandler ButtonEventHandler { get; set; }
        public EventHandler ClearFieldsEventHandler { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RepeatPassword { get; set; } = string.Empty;
        public ICommand SignInCommand { get; }
        public ICommand SignUpCommand { get; }
        public RegisterViewModel()
        {
            SignInCommand = new DelegateCommand(OnSignIn);
            SignUpCommand = new DelegateCommand(OnSignUp);
        }
        public void OnSignIn()
        {
            var authView = new AuthView();
            authView.Show();
            ButtonEventHandler?.Invoke(this, new EventArgs());
        }
        public void OnSignUp()
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(RepeatPassword))
            {
                Password = Password.Replace(" ", "");
                Username = Username.Replace(" ", "");
                var shopDB = new ShopDatabase();
                var users = shopDB.GetUsers(string.Empty);
                bool flag = false;
                foreach (var user in users)
                {
                    if (Username.ToLower() == user.Username.ToLower() || Username.ToLower() == "admin")
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    MessageBox.Show("User with this username already exists.");
                }
                else
                {
                    if (Password != RepeatPassword)
                    {
                        MessageBox.Show("Passwords are not the same.");
                    }
                    else
                    {
                        var new_user = new User() { Username = Username, Password = EncoderSHA256.ToSHA256(Password), ProfilePicture = "https://res.cloudinary.com/dw0okfdb2/image/upload/v1725724321/kcguytcpvnxzrblurfzg.png", Cash = 0.0m, Email = Email };
                        shopDB.AddUser(new_user);
                        var mainUserView = new MainUserView(new_user);
                        mainUserView.Show();
                        ButtonEventHandler?.Invoke(this, new EventArgs());
                        MessageBox.Show("Successful registration. Welcome!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Fill all boxes.");
            }
            Email = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            RepeatPassword = string.Empty;
            ClearFieldsEventHandler?.Invoke(this, new EventArgs());
        }
    }
}
