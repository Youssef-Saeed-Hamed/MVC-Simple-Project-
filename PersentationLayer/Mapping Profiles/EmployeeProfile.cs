using AutoMapper;
using DataAccessLayer.Models;
using PersentationLayer.ModelsView;

namespace PersentationLayer.Mapping_Profiles
{
	public class EmployeeProfile : Profile
	{ 
		public EmployeeProfile() {
			CreateMap<EmployeeVM, Employee>().ReverseMap();
		}
	}
}
