using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Domain.Common;

namespace HR_Management_Application.Persistence.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
        Task<bool> Exists(int id);

        // Task<T> Get(int id);
        Task<List<T>> GetAll();
    }
}