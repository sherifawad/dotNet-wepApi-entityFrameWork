using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace dotNet_wepApi_entityFrameWork.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.ApplyConfiguration(new PositionConfiguration());
            builder.ApplyConfiguration(new EmployeeConfiguration());
            base.OnModelCreating(builder);
        }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}