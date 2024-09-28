using ADOFINAL.Database;
using ADOFINAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOFINAL.ViewModels
{
    public class HistoryUserViewModel : ViewModel
    {
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

        public DataSet UserActions
        {
            get
            {
                return database.getUserActionsDataSet(CurrentUser);
            }
        }


        public HistoryUserViewModel(User user)
        {
            CurrentUser = user;
            database = new ShopDatabase();
        }
    }
}
