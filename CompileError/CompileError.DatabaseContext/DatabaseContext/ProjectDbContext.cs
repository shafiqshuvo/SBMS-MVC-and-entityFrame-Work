﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.Model.Model;


namespace CompileError.DatabaseContext.DatabaseContext
{
    public class ProjectDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchasedProduct> PurchasedProducts { get; set; }
        public DbSet<SalesDetail> SalesDetails { get; set; }
        public DbSet<Sale> Sales { get; set; }

    }
}
