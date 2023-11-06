using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI3.Entities
{
    public class RestaurantDbContext : DbContext
    {
        private string _connectionString = "Server=Y-SHANI;Database=RestaurantDb;User ID=sa;Password=!Yosi123123;";
        public DbSet<Restaurant> Restaurants  { get; set; }
        public DbSet<Address> Addresses{ get; set; }
        public DbSet<Dish> Dishes{ get; set; }
        public DbSet<Customer> Customers{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Dish>()
               .Property(d => d.Name)
               .IsRequired();
            modelBuilder.Entity<Address>()
                 .Property(d => d.City)
                 .HasMaxLength(50);
            modelBuilder.Entity<Address>()
                 .Property(d => d.Street)
                 .HasMaxLength(50);
            modelBuilder.Entity<Customer>()
                 .Property(d => d.Name)
                 .IsRequired()
                 .HasMaxLength(25);


        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
