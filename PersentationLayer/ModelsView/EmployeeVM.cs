using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace PersentationLayer.ModelsView
{
	public class EmployeeVM
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(30)]
		[MinLength(3)]
		public string Name { get; set; }
		[Range(22, 60)]
		public int Age { get; set; }
		[MaxLength(30)]
		[MinLength(3)]
		public string Address { get; set; }
		[EmailAddress]
		public string Email { get; set; }
		[Phone]
		public string Phone { get; set; }
		public bool IsActive { get; set; }
		public IFormFile? Image { get; set; }
		public string? ImageName { get; set; }
		public Department? Department { get; set; }
		public int? DeptId { get; set; }
	}
}
