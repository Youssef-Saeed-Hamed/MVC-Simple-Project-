using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.RepositaryInterfaces
{
    public interface IGenaricRepositary<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        T? GetById(int id);
        Task AddAsync(T department);
        void Update(T department);
        void Delete(T department);
    }
}
