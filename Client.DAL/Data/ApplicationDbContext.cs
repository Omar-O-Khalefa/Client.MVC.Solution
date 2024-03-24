using Client.DAL.Data.Configurations;
using Client.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Client.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base( options)
        {
            
        }
        // use when you didnt use dependancie injection
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = . ; Database = MVCClientApplication ; Trusted_Connection = True ; MultipleActiveResultSets = False;");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations());
          //modelBuilder.ApplyConfiguration<Employee>(new EmployeetConfigurations());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
