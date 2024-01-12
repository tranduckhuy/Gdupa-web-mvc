using Microsoft.EntityFrameworkCore;

namespace WarehouseWebMVC.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    }
}
