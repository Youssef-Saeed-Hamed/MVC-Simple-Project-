using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace PersentationLayer.ModelsView
{
	public class DepartmentVM
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Range(1, 10000)]
		public int Code { get; set; }
		public string Description { get; set; }
		[Required]
		public DateTime CreatedDate { get; set; }
		public ICollection<Employee> Employees { get; set; } = new List<Employee>();
	}
}
