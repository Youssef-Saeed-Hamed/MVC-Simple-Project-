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
    public class GenaricRepositary<T> : IGenaricRepositary<T> where T : class
    {
        private readonly DataContext context;

        public GenaricRepositary(DataContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(T entity)
        {
            await context.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            context.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return  (IEnumerable<T>)await context.Employees.Include(e => e.Department).ToListAsync();
            }
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {			
			return  await context.Set<T>().FindAsync(id);
        }
        public T? GetById(int id)
        {			
			return   context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            context.Update(entity);
        }
    }
}
