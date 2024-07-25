using AutoMapper;
using DataAccessLayer.Models;
using PersentationLayer.ModelsView;

namespace PersentationLayer.Mapping_Profiles
{
	public class UsersProfile : Profile
	{
		public UsersProfile() 
		{
			CreateMap<UsersVM, AppUser>().ReverseMap();
		}
	}
}
