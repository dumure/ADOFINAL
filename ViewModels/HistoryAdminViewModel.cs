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
    public class HistoryAdminViewModel : ViewModel
    {
        private ShopDatabase database;

        public DataSet UserActions
        {
            get
            {
                return database.getUserActionsDataSet();
            }
        }


        public HistoryAdminViewModel()
        {
            database = new ShopDatabase();
        }
    }
}
