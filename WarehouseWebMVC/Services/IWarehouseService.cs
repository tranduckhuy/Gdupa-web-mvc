using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services
{
    public interface IWarehouseService
    {
        WarehouseViewModel GetAll(int page);
    }
}
