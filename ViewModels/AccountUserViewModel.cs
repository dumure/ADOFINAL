using ADOFINAL.Commands;
using ADOFINAL.Encoder;
using ADOFINAL.Database;
using ADOFINAL.Image;
using ADOFINAL.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.IdentityModel.Tokens;

namespace ADOFINAL.ViewModels
{
    public class AccountUserViewModel : ViewModel
    {
        public EventHandler SaveButton;
        private ShopDatabase database;
        private User _currentUser;
        public string Username { get; set; } 
        public string Password { get; set; }
        private decimal _cash;
        public decimal Cash
        {
            get
            {
                return _cash;
            }
            set
            {
                _cash = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Cash)));
            }
        }
        private string _picture;
        public string Picture
        {
            get
            {
                return _picture;
            }
            set
            {
                if (!_picture?.Equals(value) ?? value != null)
                {
                    _picture = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Picture)));
                }
            }
        }
        public AccountUserViewModel(User user)
        {
            database = new ShopDatabase();
            _currentUser = user;
            Picture = _currentUser.ProfilePicture;
            Username = _currentUser.Username;
            Cash = _currentUser.Cash;
            SaveCommand = new DelegateCommand(Save);
            CashCommand = new DelegateCommand(AddHundredDollars);
            PictureCommand = new DelegateCommand(ChangeImage);
        }

        private void ChangeImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Select an Image"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImageUploader uploader = new ImageUploader();
                Picture = uploader.Upload(openFileDialog.FileName);
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CashCommand { get; }
        public ICommand PictureCommand { get; }
        private void Save()
        {
            if (!string.IsNullOrEmpty(Username))
            {
                Username = Username.Replace(" ", "");
                var users = database.GetUsers(string.Empty);
                bool flag = false;
                foreach (var user in users)
                {
                    if (Username.ToLower() == user.Username.ToLower() || Username.ToLower() == "admin")
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    _currentUser.Username = Username;
                }
            }
            
            Password = Password.Replace(" ", "");
            if (!string.IsNullOrEmpty(Password))
            {
                _currentUser.Password = EncoderSHA256.ToSHA256(Password);
            }
            _currentUser.Cash = Cash;
            _currentUser.ProfilePicture = Picture;
            database.UpdateUser(_currentUser);
            SaveButton?.Invoke(this, EventArgs.Empty);
        }
        private void AddHundredDollars()
        {
            Cash += 100;
        }
    }
}
