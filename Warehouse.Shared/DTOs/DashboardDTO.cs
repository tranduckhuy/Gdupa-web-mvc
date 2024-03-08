﻿namespace Warehouse.Shared.DTOs
{
    public class DashboardDTO
    {
        public int TotalProducts { get; set; }
        public int TotalSuppliers { get; set; }
        public int TotalUsers { get; set; }
        public int TotalImportNotes { get; set; }
        public int SelectedYear { get; set; }

        public ICollection<WarehouseStatistic> WarehouseStatistics { get; set; } = new List<WarehouseStatistic>();
        public ICollection<int> CurrentYearTotalImport { get; set; } = new List<int>();
        public ICollection<int> ImportYears { get; set; } = new List<int>();
    }

    public class WarehouseStatistic
    {
        public int Stock { get; set; }
        public int Import { get; set; }
        public int Export { get; set; }
    }
}
