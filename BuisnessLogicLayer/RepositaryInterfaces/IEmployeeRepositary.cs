using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.RepositaryInterfaces
{
    public interface IEmployeeRepositary : IGenaricRepositary<Employee>
    {
        public Task<IEnumerable<Employee>> GetAllByNameAsync(string Name);
        
    }
}
