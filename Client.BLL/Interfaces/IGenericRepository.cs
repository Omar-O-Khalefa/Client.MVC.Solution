using Client.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        int Add(T enity);
        int Update(T enity);
        int Delete(T enity);
    }
}
