using ADOFINAL.Commands;
using ADOFINAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ADOFINAL.Database
{
    public class ShopDatabase
    {
        private ShopContext context;
        public ShopDatabase()
        {
            context = new ShopContext();
        }
        public List<Product> GetProducts(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return context.Products.ToList();
            }
            List<Product> products = new List<Product>();
            foreach (var product in context.Products.ToList())
            {
                if (product.Name.Contains(filter))
                {
                    products.Add(product);
                }
            }
            return products;
        }
        public List<User> GetUsers(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return context.Users.ToList();
            }
            List<User> users = new List<User>();
            foreach (var user in context.Users.ToList())
            {
                if (user.Username.Contains(filter))
                {
                    users.Add(user);
                }
            }
            return users;
        }
        public List<UserAction> GetUserActions()
        {
            return context.UserActions.ToList();
        }
        public void AddProduct(Product product)
        {
            var new_product = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                ProductPicture = product.ProductPicture,
                Owner = product.Owner
            };
            context.Products.Add(new_product);
            context.SaveChanges();
        }
        public void DeleteProduct(Product product)
        {
            context.Products.Remove(context.Products.Find(product.Id));
            context.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            var required_product = context.Products.Find(product.Id);
            required_product.ProductPicture = product.ProductPicture;
            required_product.Price = product.Price;
            required_product.Name = product.Name;
            required_product.Owner = product.Owner;
            context.SaveChanges();
        }
        public void AddUser(User user)
        {
            var new_user = new User()
            {
                Username = user.Username,
                Cash = user.Cash,
                ProfilePicture = user.ProfilePicture,
                Password = user.Password,
                Email = user.Email
            };
            context.Users.Add(new_user);
            context.SaveChanges();
        }
        public void DeleteUser(User user)
        {
            context.Users.Remove(context.Users.Find(user.Id));
            context.SaveChanges();
        }
        public void UpdateUser(User user)
        {
            var required_user = context.Users.Find(user.Id);
            required_user.Username = user.Username;
            required_user.Cash = user.Cash;
            required_user.ProfilePicture = user.ProfilePicture;
            required_user.Password = user.Password;
            required_user.Email = user.Email;
            context.SaveChanges();
        }
        public void AddUserAction(User user, Product product, int state)
        {
            var user_for_action = context.Users.Find(user.Id);
            var prod_for_action = context.Products.Find(product.Id);
            if (state == 0)
            {
                prod_for_action.Owner = user.Username;
            }
            else if (state == 1)
            {
                prod_for_action.Owner = "none";
            }
            else if (state == 2)
            {
                prod_for_action.Owner = "admin";
            }
            var action = new UserAction()
            {
                Id_User = user_for_action.Id,
                Id_Product = prod_for_action.Id,
                State = state,
                DateTime = DateTime.Now
            };
            context.UserActions.Add(action);
            context.SaveChanges();
        }
        //public void DeleteUserAction(User user, Product product)
        //{
        //    var userActions = context.UserActions;
        //    foreach (var userAction in userActions)
        //    {
        //        if (userAction.User.Id == user.Id && userAction.Product.Id == product.Id)
        //        {
        //            context.UserActions.Remove(context.UserActions.Find(userAction.Id));
        //        }
        //    }
        //    context.SaveChanges();
        //}
        public void UpdateProducts(ObservableCollection<Product> products)
        {
            foreach (var product in products)
            {
                if (product is not DefaultProduct)
                {
                    UpdateProduct(product);
                }
            }
        }

        public DataSet getUserActionsDataSet(User user = null)
        {
            DataSet userActions = new DataSet();
            if (user == null)
            {
                string getUserActionsQuery = @$"USE [ShopDatabase]

SELECT U.Username AS [USERNAME], UA.State AS [0: CART 1: BOUGHT 2: REMOVED], P.[Name] AS [WHAT], UA.[DateTime] AS [WHEN]
FROM UserActions AS UA
JOIN Users AS U ON UA.Id_User = U.Id
JOIN Products AS P ON UA.Id_Product = P.Id
ORDER BY UA.[DateTime] DESC";
                var sqlDataAdapter = new SqlDataAdapter(getUserActionsQuery, context.ConnectionString);
                var sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.Fill(userActions, "actions");
            }
            else
            {
                string getUserActionsByUserQuery = $@"USE [ShopDatabase]

SELECT U.Username AS [USERNAME], UA.State AS [0: CART 1: BOUGHT 2: REMOVED], P.[Name] AS [WHAT], UA.[DateTime] AS [WHEN]
FROM UserActions AS UA
JOIN Users AS U ON UA.Id_User = U.Id
JOIN Products AS P ON UA.Id_Product = P.Id
WHERE U.Id = {user.Id}
ORDER BY UA.[DateTime] DESC";
                var sqlDataAdapter = new SqlDataAdapter(getUserActionsByUserQuery, context.ConnectionString);
                var sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.Fill(userActions, "actions");
            }
            return userActions;
        }
        //public void UpdateUsers(IList<User> users)
        //{
        //    context.RemoveRange(context.Users.Where(u => true));
        //    foreach (var user in users)
        //    {
        //        user.Id = 0;
        //        context.Users.Add(user);
        //    }
        //    context.SaveChanges();
        //}
       
        //public void UpdateUserActions(ICollection<UserAction> userActions)
        //{
        //    context.RemoveRange(context.UserActions.Where(ua => true));
        //    foreach (var userAction in userActions)
        //    {
        //        userAction.Id = 0; 
        //        context.UserActions.Add(userAction);
        //    }
        //    context.SaveChanges();
        //}
    }
}
