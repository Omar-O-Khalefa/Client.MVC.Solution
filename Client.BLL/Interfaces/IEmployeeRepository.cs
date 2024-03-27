using Client.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        //IEnumerable<Employee> GetAll();
        //Employee Get(int id);
        //int Add(Employee enity);
        //int Update(Employee enity);
        //int Delete(Employee enity);
        IQueryable<Employee> SearchByName(string name);
        IQueryable<Employee> GetEmployeesByAdress(string adress);
    }
}
