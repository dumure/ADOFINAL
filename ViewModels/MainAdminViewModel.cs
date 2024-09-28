using ADOFINAL.Commands;
using ADOFINAL.Database;
using ADOFINAL.Image;
using ADOFINAL.Models;
using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADOFINAL.ViewModels
{
    public class MainAdminViewModel : ViewModel
    {
        public ObservableCollection<Product> Products { get; set; }
        private ShopDatabase database;
        private string defaultImage;
        public MainAdminViewModel()
        {
            database = new ShopDatabase();
            defaultImage = "https://res.cloudinary.com/dw0okfdb2/image/upload/v1725746931/agqmcqdi5qwkzdtsqxlt.jpg";
            Products = new ObservableCollection<Product>();
            LoadProducts();
        }
        public void LoadProducts(string search = "")
        {
            Products.Clear();
            DefaultProduct defaultProduct = new DefaultProduct();
            defaultProduct.ProductPicture = defaultImage;
            defaultProduct.AddCommand = new DelegateCommand(() => { AddProduct(defaultProduct); });
            defaultProduct.ChangeCommand = new DelegateCommand(() => { ChangeImage(defaultProduct); });
            Products.Add(defaultProduct);
            var products = database.GetProducts(search);
            for (int i = 0; i < products.Count; i++)
            {
                var product = products[i];
                if (product.Owner == "admin")
                {
                    product.AddCommand = new DelegateCommand(() => { });
                    product.ChangeCommand = new DelegateCommand(() => { ChangeImage(product); });
                    product.UpdateCommand = new DelegateCommand(() => { UpdateProduct(product); });
                    product.DeleteCommand = new DelegateCommand(() => { DeleteProduct(product); });
                    Products.Add(product);
                }
            }
        }

        private void DeleteProduct(Product product)
        {
            database.DeleteProduct(product);
            defaultImage = "https://res.cloudinary.com/dw0okfdb2/image/upload/v1725746931/agqmcqdi5qwkzdtsqxlt.jpg";
            LoadProducts();
        }

        private void AddProduct(Product product)
        {
            if (!string.IsNullOrEmpty(product.Name) && product.Price > 0 && product.ProductPicture != "https://res.cloudinary.com/dw0okfdb2/image/upload/v1725746931/agqmcqdi5qwkzdtsqxlt.jpg")
            {
                product.Owner = "admin";
                database.AddProduct(product);
                defaultImage = "https://res.cloudinary.com/dw0okfdb2/image/upload/v1725746931/agqmcqdi5qwkzdtsqxlt.jpg";
                LoadProducts();
            }
        }

        private void UpdateProduct(Product product)
        {
            if (!string.IsNullOrEmpty(product.Name) && product.Price > 0)
            {
                database.UpdateProduct(product);
                LoadProducts();
            }
        }
        public void ChangeImage(Product product)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Select an Image"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImageUploader uploader = new ImageUploader();
                if (product is DefaultProduct)
                {
                    defaultImage = uploader.Upload(openFileDialog.FileName);
                    LoadProducts();
                }
                else
                {
                    product.ProductPicture = uploader.Upload(openFileDialog.FileName);
                    UpdateProduct(product);
                }
            }
        }
    }
}
