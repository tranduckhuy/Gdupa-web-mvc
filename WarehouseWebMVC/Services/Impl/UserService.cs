using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.Service;

namespace WarehouseWebMVC.Services.Impl;

public class UserService : IUserService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public UserService(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public bool CheckLogin(UserDTO userDTO)
    {
        var u = _dataContext.Users.SingleOrDefault(u => u.Email == userDTO.Email);

        if (u != null && u.Password == userDTO.Password)
        {
            return true;
        }

        return false;
    }
}
