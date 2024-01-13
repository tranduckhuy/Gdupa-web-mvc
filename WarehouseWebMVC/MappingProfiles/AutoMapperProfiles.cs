using AutoMapper;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs;

namespace WarehouseWebMVC.MappingProfiles
{
	public class AutoMapperProfiles : Profile
	{
        public AutoMapperProfiles()
        {
			CreateMap<Product, ProductDTO>().ReverseMap();
		}
    }
}
