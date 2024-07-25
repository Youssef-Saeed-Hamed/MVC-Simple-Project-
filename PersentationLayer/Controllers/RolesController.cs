using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersentationLayer.ModelsView;

namespace PersentationLayer.Controllers
{
	public class RolesController : Controller
	{
		private readonly RoleManager<IdentityRole> _RoleManager;
		private readonly IMapper mapper;

		public RolesController(RoleManager<IdentityRole> roleManager, IMapper mapper)
		{
			_RoleManager = roleManager;
			this.mapper = mapper;
		}

		public async Task<IActionResult> Index(string RoleName)
		{
			if (string.IsNullOrWhiteSpace(RoleName))
			{
				var Roles = await _RoleManager.Roles.ToListAsync();
				var MappedRoles = mapper.Map<IEnumerable<RolesVM>>(Roles);
				return View(MappedRoles);
			}
			else
			{
				var Role = await _RoleManager.Roles.FirstOrDefaultAsync(r=> r.Name.ToLower().Contains(RoleName.ToLower()));
				if (Role is null) return View(Enumerable.Empty<RolesVM>());
				var MappedRole = mapper.Map<RolesVM>(Role);
				return View(new List<RolesVM> { MappedRole});
			}
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(RolesVM model)
		{
			if (ModelState.IsValid)
			{
				var MappedRole = mapper.Map<IdentityRole>(model);
				var result = await _RoleManager.CreateAsync(MappedRole);
				if (result.Succeeded) 
					return RedirectToAction(nameof(Index));
                foreach (var error in result.Errors)
                {
					ModelState.AddModelError("",error.Description);
                }
            }
			return View(model);
		}
		public async Task<IActionResult> Details(string Id)
		{
			var role = await _RoleManager.FindByIdAsync(Id);
			var MappedRole = mapper.Map<RolesVM>(role);
			return View(MappedRole);
		}

		public async Task<IActionResult> Edit(string Id)
		{
			var role = await _RoleManager.FindByIdAsync(Id);
			var MappedRole = mapper.Map<RolesVM>(role);
			return View(MappedRole);
		}

		[HttpPost]

		public async Task<IActionResult> Edit (string Id , RolesVM model)
		{
			if (Id != model.Id) 
				return BadRequest();
			if (!ModelState.IsValid) 
				return View(model);
			try
			{
				var role = await _RoleManager.FindByIdAsync (Id);
				if (role is null) 
					return NotFound();
				role.Name = model.Name;
				await _RoleManager.UpdateAsync(role);
				return RedirectToAction(nameof(Index));
				
			}
			catch(Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(model);
			}
		}
		public async Task<IActionResult> Delete(string Id)
		{
			var role = await _RoleManager.FindByIdAsync(Id);
			var MappedRole = mapper.Map<RolesVM>(role);
			return View(MappedRole);
		}

		[HttpPost]

		public async Task<IActionResult> ConfirmDelete (string Id)
		{		
			try
			{
				var role = await _RoleManager.FindByIdAsync (Id);
				if (role is null) 
					return NotFound();
				await _RoleManager.DeleteAsync(role);
				return RedirectToAction(nameof(Index));
				
			}
			catch(Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}
		}
	}
}
