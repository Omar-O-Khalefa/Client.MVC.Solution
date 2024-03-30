using Client.BLL.Interfaces;
using Client.DAL.Data;
using Client.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Client.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbcontext;

        //Ask Clr For Creating Object From (ApplicationDbContext)
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public void Add(T enity)
        {
            _dbcontext.Set<T>().Add(enity);
            //  _dbcontext.Add(enity);  EF Core 3.1 New Feature
           // return _dbcontext.SaveChanges();
        }
        public void Update(T enity)
        {
            _dbcontext.Set<T>().Update(enity);
            //  _dbcontext.Update(enity);  EF Core 3.1 New Feature
           // return _dbcontext.SaveChanges();
        }
        public void Delete(T enity)
        {
            _dbcontext.Set<T>().Remove(enity);
            //return _dbcontext.SaveChanges();
        }
        public T Get(int id)
        {
            ///var Employe =  _dbcontext.Set<T>().Local.Where(x => x.Id == id).FirstOrDefault();
            ///
            ///if(Employe == null)
            ///{
            ///    Employe = _dbcontext.Set<T>().Where(x => x.Id == id).FirstOrDefault();
            ///}
            ///return Employe;
            //return _dbcontext.Find<T>(id);

            return _dbcontext.Set<T>().Find(id);
        }
        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)_dbcontext.Employees.Include(E => E.Department).AsNoTracking();
            }
            else
            {
                return _dbcontext.Set<T>().AsNoTracking().ToList();
            }
        }

    }
}
