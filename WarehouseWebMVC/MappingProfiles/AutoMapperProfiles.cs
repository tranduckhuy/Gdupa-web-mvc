using AutoMapper;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs.ProductDTO;

namespace WarehouseWebMVC.MappingProfiles;

public class AutoMapperProfiles : Profile
	{
    public AutoMapperProfiles()
    {
		CreateMap<Product, ProductDTO>().ReverseMap();
		CreateMap<Product, AddProductDTO>().ReverseMap();
    }
}
