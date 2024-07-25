using BuisnessLogicLayer.RepositaryInterfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Repositary
{
    public class EmployeeRepositary : GenaricRepositary<Employee> , IEmployeeRepositary
    {
		private readonly DataContext context;

		public EmployeeRepositary(DataContext context) : base(context)
		{
			this.context = context;
		}

		public async Task <IEnumerable<Employee>> GetAllByNameAsync(string Name)
		{
			return await context.Employees.Where(e => e.Name.ToLower().Contains(Name.ToLower())).ToListAsync();
		}
	}
}
