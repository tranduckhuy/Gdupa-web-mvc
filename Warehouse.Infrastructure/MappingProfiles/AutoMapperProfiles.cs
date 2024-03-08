using AutoMapper;
using Warehouse.Shared.DTOs.UserDTO;
using Warehouse.Domain.Entities;
using Warehouse.Shared.DTOs;
using Warehouse.Shared.DTOs.ProductDTO;

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
