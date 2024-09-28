using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOFINAL.Models
{
    public class UserAction
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int Id_User { get; set; }
        public Product Product { get; set; }
        public int Id_Product { get; set; }
        public DateTime DateTime { get; set; }
        public int State { get; set; } // 0 added to cart 1 bought 2 removed from cart
    }
}
