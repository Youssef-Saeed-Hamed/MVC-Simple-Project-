using AutoMapper;
using DataAccessLayer.Models;
using PersentationLayer.ModelsView;

namespace PersentationLayer.Mapping_Profiles
{
	public class DepartmentProfile : Profile
	{
        public DepartmentProfile()
        {
            CreateMap<DepartmentVM,Department>().ReverseMap();
        }
    }
}
