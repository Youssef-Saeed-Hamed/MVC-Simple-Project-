using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.RepositaryInterfaces
{
	public interface IUnitOfWork
	{
		public IDepartmentReposiatry Departments { get; }
		public IEmployeeRepositary Employees { get; }

		public Task<int> CompleteAsync();
	}
}
