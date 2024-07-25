using BuisnessLogicLayer.RepositaryInterfaces;
using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Repositary
{
	public class UnitOfWork : IUnitOfWork
	{

		private readonly IDepartmentReposiatry departments;
		private readonly IEmployeeRepositary employees;
		private readonly DataContext context;
		public UnitOfWork(DataContext context)
		{
			this.departments = new DepartmentReposiatry(context);
			this.employees = new EmployeeRepositary(context);
			this.context = context;
		}

		public IDepartmentReposiatry Departments => departments;

		public IEmployeeRepositary Employees => employees;
		public async Task<int> CompleteAsync()
		{
			return await this.context.SaveChangesAsync();
		}
	}
}
