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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public DepartmentRepository( ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public int Add(Department enity)
        {
            _dbcontext.Departmentss.Add(enity);
            return _dbcontext.SaveChanges();
        }
        public int Update(Department enity)
        {
            _dbcontext.Update(enity);
            return _dbcontext.SaveChanges();
        }
        public int Delete(Department enity)
        {
            _dbcontext.Departmentss.Remove(enity);
            return _dbcontext.SaveChanges();
        }
        public Department Get(int id)
        {
            ///var Depart =  _dbcontext.Departments.Local.Where(x => x.Id == id).FirstOrDefault();
            ///
            ///if(Depart == null)
            ///{
            ///    Depart = _dbcontext.Departments.Where(x => x.Id == id).FirstOrDefault();
            ///}
            ///return Depart;
            //return _dbcontext.Find<Department>(id);
                
            return _dbcontext.Departmentss.Find(id);
        }
        public IEnumerable<Department> GetAll()
        {
            return _dbcontext.Departmentss.AsNoTracking().ToList();
        }


    }
}
