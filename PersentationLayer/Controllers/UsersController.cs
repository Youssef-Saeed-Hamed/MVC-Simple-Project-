using AutoMapper;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersentationLayer.ModelsView;

namespace PersentationLayer.Controllers
{
	public class UsersController : Controller
	{
		private readonly UserManager<AppUser> _UserManger;
		private readonly IMapper mapper;


		public UsersController(UserManager<AppUser> userManger, IMapper mapper)
		{
			_UserManger = userManger;
			this.mapper = mapper;
		}

		public async Task<IActionResult> Index(string? email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				var users = await _UserManger.Users.ToListAsync();
				var MappedUsers = mapper.Map<IEnumerable<UsersVM>>(users).ToList();
				for(var i = 0; i < users.Count; i++)
				{
					MappedUsers[i].Roles = await _UserManger.GetRolesAsync(users[i]);
				}
				return View(MappedUsers);
			}
			else
			{
				var user = await _UserManger.FindByEmailAsync(email);
				if (user == null)
					return View(Enumerable.Empty<UsersVM>());
				var MappedUser = mapper.Map<AppUser,UsersVM>(user);
				return View(new List<UsersVM> { MappedUser});
			}			
		}

		public async Task<IActionResult> Details(string Id)
		{
			var user = await _UserManger.FindByIdAsync(Id);
			var mappedUser = mapper.Map<UsersVM>(user);
			return View(mappedUser);
		}

		public async Task<IActionResult> Delete(string Id)
		{
			var user = await _UserManger.FindByIdAsync(Id);
			var mappedUser = mapper.Map<UsersVM>(user);
			return View(mappedUser);
		}
		public async Task<IActionResult> Edit(string Id)
		{
			var user = await _UserManger.FindByIdAsync(Id);
			var mappedUser = mapper.Map<UsersVM>(user);
			return View(mappedUser);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(string id , UsersVM model)
		{
			if (id != model.Id)
				return BadRequest();
			if (!ModelState.IsValid)
				return View(model);
			try
			{
				var user = await _UserManger.FindByIdAsync(id);
				if (user is null)
					return NotFound();
				user.FirstName = model.FirstName;
				user.LastName = model.LastName;
				await _UserManger.UpdateAsync(user);
				return RedirectToAction(nameof(Index));
			}
			catch(Exception ex) 
			{
				ModelState.AddModelError("", ex.Message);
				return View(model);
			}
		}
		[HttpPost]
		public async Task<IActionResult> ConfirmDelete(string id )
		{
			
			try
			{
				var user = await _UserManger.FindByIdAsync(id);
				if (user is null)
					return NotFound();
				
				await _UserManger.DeleteAsync(user);
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
