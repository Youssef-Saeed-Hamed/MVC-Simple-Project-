using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PersentationLayer.ModelsView;

namespace PersentationLayer.Mapping_Profiles
{
	public class RolesProfile : Profile
	{
		public RolesProfile() { 
			CreateMap<RolesVM , IdentityRole>().ReverseMap();
		}
	}
}
