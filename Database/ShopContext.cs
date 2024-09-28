using ADOFINAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOFINAL.Database
{
    public class ShopContext : DbContext
    {
        string _connectionString;
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAction> UserActions { get; set; }
        public ShopContext()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(@$"{AppContext.BaseDirectory}..\..\..\Database")
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Owner).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ProductPicture).HasMaxLength(200);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Ignore(e => e.BuyCommand);
                entity.Ignore(e => e.ChangeCommand);
                entity.Ignore(e => e.UpdateCommand);
                entity.Ignore(e => e.AddCommand);
                entity.Ignore(e => e.DeleteCommand);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ProfilePicture).HasMaxLength(200);
                entity.Property(e => e.Cash).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<UserAction>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id_User).IsRequired();
                entity.Property(e => e.Id_Product).IsRequired();
                entity.Property(e => e.DateTime).IsRequired();
                entity.Property(e => e.State).IsRequired();
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.Id_User);

                entity.HasOne(e => e.Product)
                      .WithMany()
                      .HasForeignKey(e => e.Id_Product);
            });
        }
    }
}
