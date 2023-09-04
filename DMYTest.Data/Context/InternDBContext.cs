﻿namespace DMYTest.Data.Context
{
    
    #region Usings
using DMYTest.Data.Models;
    using System.Data.Entity;

    #endregion

    public class InternDBContext : DbContext
    {
        public InternDBContext() :base("InternDBContext")
        { }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<ProductImage> ProductImages { get; set; }
        public IDbSet<Supplier> Suppliers { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Sales> Sales { get; set; }
        public IDbSet<Cart> Carts { get; set; }
    }
}