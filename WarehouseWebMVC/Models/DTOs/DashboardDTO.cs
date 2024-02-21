namespace WarehouseWebMVC.Models.DTOs
{
    public class DashboardDTO
    {
        public int TotalProducts { get; set; } = 1;
        public int TotalSuppliers { get; set; } = 2;
        public int TotalUsers { get; set; }
        public int TotalImportNotes { get; set; }

        public ICollection<WarehouseStatistic> WarehouseStatistics { get; set; } = new List<WarehouseStatistic>();
    }

    public class WarehouseStatistic
    {
        public int Stock { get; set; }
        public int Import { get; set; }
        public int Export { get; set; }
    }
}
