using AutoMapper;
using Warehouse.Domain.DTOs;
using Warehouse.Domain.DTOs.ProductDTO;
using Warehouse.Domain.DTOs.UserDTO;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.MappingProfiles;

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
