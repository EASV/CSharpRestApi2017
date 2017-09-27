using CustomerAppDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace CustomerAppDAL.Context
{
    class CustomerAppContext : DbContext
    {
        static DbContextOptions<CustomerAppContext> options =
            new DbContextOptionsBuilder<CustomerAppContext>()
                         .UseInMemoryDatabase("TheDB")
                         .Options;

        //Options That we want in Memory
        //      public CustomerAppContext() : base(options)
        //      {
        //}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=tcp:easv-cs.database.windows.net,1433;Initial Catalog=CS2017;Persist Security Info=False;User ID=username;Password=password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerAddress>()
                .HasKey(ca => new { ca.AddressId, ca.CustomerId });

            modelBuilder.Entity<CustomerAddress>()
                .HasOne(ca => ca.Address)
                .WithMany(a => a.Customers)
                .HasForeignKey(ca => ca.AddressId);

            modelBuilder.Entity<CustomerAddress>()
                .HasOne(ca => ca.Customer)
                .WithMany(c => c.Addresses)
                .HasForeignKey(ca => ca.CustomerId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}