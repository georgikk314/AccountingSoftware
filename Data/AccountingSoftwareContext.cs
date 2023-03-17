using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AccountingSoftware.Data
{
    public class AccountingSoftwareContext : DbContext
    {
        
        public AccountingSoftwareContext()
        {
            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "AccountingSoftware.db3");

            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }
        
    }
}
