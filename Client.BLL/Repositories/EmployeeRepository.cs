using Client.BLL.Interfaces;
using Client.DAL.Data;
using Client.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        //private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext):base(dbContext) // Asking Clr For Creating Object From DbContext
        {
            //_dbContext = dbContext;
        }
        public IQueryable<Employee> GetEmployeesByAdress(string adress)
        {
            return _dbcontext.Employees.Where(E => E.Adress.ToLower() == adress.ToLower());
                
        }
        // Benefits Of Generic Repository Pattern
        // 1.We Create An Abstrct Layer Between Data Access layer (Dbcontext) And Presentation  Layer (Controllers)
        // 2.Reduces  Code Duplicate 
        // 3.Code Is Cleaner  And Easier To Maintain And Reuse
        // 4.Support Loosly Coupled Approach
        // 5.Support Dependency  Injection
        // 6.Support Unit Testing 
      
    }
}
