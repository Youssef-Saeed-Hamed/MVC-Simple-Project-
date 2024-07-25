
using BuisnessLogicLayer.RepositaryInterfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Repositary
{
    public class DepartmentReposiatry : GenaricRepositary<Department>,IDepartmentReposiatry
    {

        public DepartmentReposiatry(DataContext context) : base(context) { }
       

    }
}
