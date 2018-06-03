using DataCollectionModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DataCollectionModel
{
    public class AppDbContext : DbContext
    {
        static AppDbContext()
        {
            Database.SetInitializer(new AppDbInit());
        }

        public AppDbContext() : base("MyDB") { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Reward> Rewards { get; set; }
    }
}
