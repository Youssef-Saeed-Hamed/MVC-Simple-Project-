using System.ComponentModel;

namespace PersentationLayer.ModelsView
{
	public class RolesVM
	{
		public string Id { get; set; }
		[DisplayName("Role")]
		public string Name { get; set; }
		public RolesVM() { 
			Id = Guid.NewGuid().ToString();
		}
	}
}
