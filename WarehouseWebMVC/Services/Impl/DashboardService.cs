using Microsoft.EntityFrameworkCore;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models.DTOs;

namespace WarehouseWebMVC.Services.Impl
{
	public class DashboardService : IDashboardService
	{
		private readonly DataContext _dataContext;

		public DashboardService(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public async Task<DashboardDTO> GetDashboardInfoAsync(int year)
		{
            DashboardDTO dashboardDTO = new()
            {
                TotalProducts = await _dataContext.Products.CountAsync(),
                TotalImportNotes = await _dataContext.ImportNotes.CountAsync(),
                TotalUsers = await _dataContext.Users.CountAsync(),
                TotalSuppliers = await _dataContext.Suppliers.CountAsync()
            };

            if (year == 0)
			{
				year = DateTime.UtcNow.Year;
			}

            int currentYear = DateTime.UtcNow.Year;
            for (int i = 1; i <= 4; i++)
			{
				DateTime startDate = new(year, (i - 1) * 3 + 1, 1);
				DateTime endDate = startDate.AddMonths(3).AddDays(-1);

				int sumQuantityImport = await _dataContext.Warehouse
					.Where(w => w.CreatedAt >= startDate && w.CreatedAt <= endDate)
					.SumAsync(qi => qi.QuantityImport);
				int stock = await _dataContext.Warehouse
					.Where(w => w.CreatedAt >= startDate && w.CreatedAt <= endDate)
					.SumAsync(q => q.Quantity);

                dashboardDTO.WarehouseStatistics.Add(new WarehouseStatistic { Stock = stock, Import = sumQuantityImport });

                startDate = new(currentYear, (i - 1) * 3 + 1, 1);
				endDate = startDate.AddMonths(3).AddDays(-1);
                sumQuantityImport = await _dataContext.Warehouse
                    .Where(w => w.CreatedAt >= startDate && w.CreatedAt <= endDate)
                    .SumAsync(qi => qi.QuantityImport);

				dashboardDTO.CurrentYearTotalImport.Add(sumQuantityImport);
			}

			var uniqueYears = await _dataContext.Warehouse
            .Select(w => w.CreatedAt.Year)
            .Distinct()
			.OrderBy(w => w)
            .ToListAsync();

			dashboardDTO.SelectedYear = year;
			dashboardDTO.ImportYears = uniqueYears;

			return dashboardDTO;
		}

	}
}
