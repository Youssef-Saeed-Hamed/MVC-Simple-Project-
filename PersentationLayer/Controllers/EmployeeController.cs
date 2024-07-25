using AutoMapper;
using BuisnessLogicLayer.RepositaryInterfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersentationLayer.ModelsView;
using PersentationLayer.Utilities;

namespace PersentationLayer.Controllers
{
	[Authorize]

	public class EmployeeController : Controller
    {
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string ? SearchValue)
        {
            if(string.IsNullOrWhiteSpace(SearchValue))
            {
				var employees = await unitOfWork.Employees.GetAllAsync();
				return View(mapper.Map<IEnumerable<EmployeeVM>>(employees));
			}
            else
            {
                var employees = await unitOfWork.Employees.GetAllByNameAsync(SearchValue);
                return View(mapper.Map<IEnumerable<EmployeeVM>>(employees));
            }
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await unitOfWork.Departments.GetAllAsync();
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(EmployeeVM employeeVm)
        {
            if (ModelState.IsValid)
            {
                employeeVm.ImageName = DocumentSettings.UploadFile(employeeVm.Image, "Images");
                var employee = mapper.Map<EmployeeVM, Employee>(employeeVm);
                
                if (employee is not null)
                {
                    await unitOfWork.Employees.AddAsync(employee);
					TempData["Message"] = "This Employee Created Successfully";
				}
				await unitOfWork.CompleteAsync();

				return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = await unitOfWork.Departments.GetAllAsync();
            return View(employeeVm);
        }

        public async Task< IActionResult> Details(int? id)
        {
            
            ViewBag.Departments = await unitOfWork.Departments.GetAllAsync();
			return await ReturnViewWithEmployee(id, nameof(Details));

        }

         public async Task<IActionResult> Edit(int? id)
		{
			ViewBag.Departments = await unitOfWork.Departments.GetAllAsync();
			return await ReturnViewWithEmployee(id, nameof(Edit));

		}

		[HttpPost]

        public async Task<IActionResult> Edit(EmployeeVM employeeVM ,[FromRoute] int id )
        {
            if (id != employeeVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
			{
				try
				{
                    
                    unitOfWork.Employees.Update(mapper.Map<Employee>(employeeVM));
					await unitOfWork.CompleteAsync();

					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
                    ModelState.AddModelError("", ex.Message);
				}
			}
			ViewBag.Departments = await unitOfWork.Departments.GetAllAsync();

			return View(employeeVM);                       
        }

        public async Task<IActionResult> Delete(int? id)
        {
			return await ReturnViewWithEmployee(id, nameof(Delete));
        }


		[HttpPost]

		public async Task<IActionResult> Delete(EmployeeVM employeeVM, [FromRoute] int id)
		{
			if (id != employeeVM.Id)
				return BadRequest();

			if (ModelState.IsValid)
			{
				try
				{
					unitOfWork.Employees.Delete(mapper.Map<Employee>(employeeVM));
                    if(await unitOfWork.CompleteAsync() > 0)
                    {
                        DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");
                    }

					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", ex.Message);
				}
			}
			return View(employeeVM);
		}


		public async Task<IActionResult> ReturnViewWithEmployee(int? id ,string ViewName)
        {
            if (!id.HasValue)
                return NotFound();
            var employee = await unitOfWork.Employees.GetByIdAsync(id.Value);
            if (employee is null)
                return BadRequest();
            if (employee.DeptId.HasValue)
            {               
				ViewBag.Dept =  unitOfWork.Departments.GetById(employee.DeptId.Value)?.Name??"No_Department";
			}
            else
            {
                ViewBag.Dept = "No_Department";
            }

			return View(ViewName,mapper.Map<EmployeeVM>(employee));
        }
    }
}
