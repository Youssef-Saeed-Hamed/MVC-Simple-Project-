using AutoMapper;
using BuisnessLogicLayer.RepositaryInterfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersentationLayer.ModelsView;

namespace PersentationLayer.Controllers
{
	[Authorize]

	public class DepartmentController : Controller
    {
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;
        public DepartmentController( IUnitOfWork unitOfWork , IMapper mapper)
        {
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await unitOfWork.Departments.GetAllAsync();            
            return View(mapper.Map<IEnumerable<DepartmentVM>>(departments));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentVM departmentVM)
        {
            var department = mapper.Map<Department>(departmentVM);
            if (ModelState.IsValid)
            {

                await unitOfWork.Departments.AddAsync(department);
                await unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }

        public async Task<IActionResult> Details(int? id)
            => await ReturnViewWithDepartment(id, nameof(Details));
        public async Task<IActionResult> Edit(int? id)
            => await ReturnViewWithDepartment(id , nameof(Edit));

        [HttpPost]

        public async Task<IActionResult> Edit(DepartmentVM departmentVM , [FromRoute]int id)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var department = mapper.Map<Department>(departmentVM);
                    unitOfWork.Departments.Update(department);
                    await unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message); 
                }
            }
            return View(departmentVM);
        }

        public async Task< IActionResult> Delete(int? id) 
            => await ReturnViewWithDepartment(id , nameof (Delete));

        [HttpPost]

        public async Task<IActionResult> Delete(DepartmentVM departmentVM ,[FromRoute]int id )
        {
            if(id != departmentVM.Id)
                return BadRequest();
            

            if (ModelState.IsValid)
            {
                try
                {
                    var department = mapper.Map<Department>(departmentVM);
                    unitOfWork.Departments.Delete(department);
                    await unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }catch (Exception ex)
                {
                    ModelState.AddModelError("",ex.Message);
                }
            }
            return View(departmentVM);
        }

        private async Task<IActionResult> ReturnViewWithDepartment(int ? id , string ViewName)
        {
            if(!id.HasValue)
                return NotFound();
            var department = await unitOfWork.Departments.GetByIdAsync(id.Value);
            if(department is null)
                return BadRequest();
            return View(ViewName , mapper.Map<DepartmentVM>(department));
        }
    }
}
