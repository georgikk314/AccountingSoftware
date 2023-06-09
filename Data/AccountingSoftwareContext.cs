﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingSoftware.Data.Tables;
using Microsoft.EntityFrameworkCore;

namespace AccountingSoftware.Data
{
    public class AccountingSoftwareContext : DbContext, IAccountingSoftwareContext
    {
        public AccountingSoftwareContext(DbContextOptions<AccountingSoftwareContext> options)
            : base(options) { }

        public AccountingSoftwareContext()
        {
            SQLitePCL.Batteries_V2.Init();
            //this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "AccountingSoftware.db3");

            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }
        
        public DbSet<Users> Users { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Vendors> Vendors { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
    }
}
