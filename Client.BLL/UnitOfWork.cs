using Client.BLL.Interfaces;
using Client.BLL.Repositories;
using Client.DAL.Data;
using Client.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BLL
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly ApplicationDbContext _dbContext;
        //public IEmployeeRepository EmployeeRepository { get; set; } = null;
        //public IDepartmentRepository DepartmentRepository { get; set; } = null;

        public UnitOfWork(ApplicationDbContext dbContext) // Ask Clr For Create Object From ApplicationDbContext
        {
            _dbContext = dbContext;

            //EmployeeRepository = new EmployeeRepository(_dbContext);

            //DepartmentRepository = new DepartmentRepository(_dbContext);
        }
        public int Complete()
        {
          return  _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IGenericRepository<T> Repository<T>() where T : ModelBase
        {
            var Key = typeof(T).Name;
        }
    }
}
