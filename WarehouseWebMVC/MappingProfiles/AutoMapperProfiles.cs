using AutoMapper;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.Models.DTOs.ProductDTO;
using WarehouseWebMVC.Models.DTOs.SupplierDTO;
using WarehouseWebMVC.Models.DTOs.UserDTO;

namespace WarehouseWebMVC.MappingProfiles;

public class AutoMapperProfiles : Profile
	{
    public AutoMapperProfiles()
    {
		CreateMap<Product, ProductDTO>().ReverseMap();
		CreateMap<Product, AddProductDTO>().ReverseMap();
		CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, AddUserDTO>().ReverseMap();
        CreateMap<User, UserInformationDTO>().ReverseMap();
        CreateMap<ImportNote, ImportNoteDTO>().ReverseMap();
        CreateMap<Supplier, SupplierDTO>().ReverseMap();
    }
}
