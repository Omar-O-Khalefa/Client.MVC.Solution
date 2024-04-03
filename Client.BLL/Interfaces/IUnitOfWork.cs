using Client.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BLL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        //public IEmployeeRepository EmployeeRepository { get; set; }
        //public IDepartmentRepository DepartmentRepository { get; set; }

        IGenericRepository<T> Repository<T>() where T : ModelBase;
        Task<int> Complete();
    }
}
