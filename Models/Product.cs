using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ADOFINAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public string ProductPicture { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ICommand BuyCommand { get; set; }
        public ICommand ChangeCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
    }
}
