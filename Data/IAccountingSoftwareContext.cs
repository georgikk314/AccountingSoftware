using AccountingSoftware.Data.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware.Data
{
    public interface IAccountingSoftwareContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Vendors> Vendors { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
    }
}
