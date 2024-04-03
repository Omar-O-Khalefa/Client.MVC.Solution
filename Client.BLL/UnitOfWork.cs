using Client.BLL.Interfaces;
using Client.BLL.Repositories;
using Client.DAL.Data;
using Client.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        //private Dictionary<string,IGenericRepository<ModelBase>> _repositoties;


        private readonly ApplicationDbContext _dbContext;

        private Hashtable _repositoties;
        //public IEmployeeRepository EmployeeRepository { get; set; } = null;
        //public IDepartmentRepository DepartmentRepository { get; set; } = null;

        public UnitOfWork(ApplicationDbContext dbContext) // Ask Clr For Create Object From ApplicationDbContext
        {
            _dbContext = dbContext;
            _repositoties = new Hashtable();

            //EmployeeRepository = new EmployeeRepository(_dbContext);

            //DepartmentRepository = new DepartmentRepository(_dbContext);
        }
        public async Task<int> Complete()
        {
          return await _dbContext.SaveChangesAsync();
        }
        public async ValueTask DisposeAsync()
        {
          await  _dbContext.DisposeAsync();
        }

     

        public IGenericRepository<T> Repository<T>() where T : ModelBase
        {
            var Key = typeof(T).Name;
            if (!_repositoties.ContainsKey(Key))
            {
               if(Key == nameof(Employee))
                {
                    var repository = new EmployeeRepository(_dbContext);
                    _repositoties.Add(Key, repository);
                }
                else
                {
                    var repository = new GenericRepository<T>(_dbContext);
                    _repositoties.Add(Key, repository);
                }
            }
            return _repositoties[Key] as IGenericRepository<T>;
        }
    }
}
