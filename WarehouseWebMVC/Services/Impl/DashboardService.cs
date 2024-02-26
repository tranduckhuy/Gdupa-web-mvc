using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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
			DashboardDTO dashboardDTO = new();
			dashboardDTO.TotalProducts = await _dataContext.Products.CountAsync();
			dashboardDTO.TotalImportNotes = await _dataContext.ImportNotes.CountAsync();
			dashboardDTO.TotalUsers = await _dataContext.Users.CountAsync();
			dashboardDTO.TotalSuppliers = await _dataContext.Suppliers.CountAsync();

			if (year == 0)
			{
				year = DateTime.UtcNow.Year;
			}

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
				dashboardDTO.CurrentYearTotalImport.Add(sumQuantityImport);
			}

			var uniqueYears = await _dataContext.Warehouse
            .Select(w => w.CreatedAt.Year)
            .Distinct()
            .ToListAsync();

			dashboardDTO.ImportYears = uniqueYears;

			return dashboardDTO;
		}

		public DashboardDTO GetDashboardInfo(int year)
		{
			DashboardDTO dashboardDTO = new();
			dashboardDTO.TotalProducts = _dataContext.Products.Count();
			dashboardDTO.TotalImportNotes = _dataContext.ImportNotes.Count();
			dashboardDTO.TotalUsers = _dataContext.Users.Count();
			dashboardDTO.TotalSuppliers = _dataContext.Suppliers.Count();
			Thread.Sleep(2000);

			if (year == 0)
			{
				year = DateTime.UtcNow.Year;
			}

			for (int i = 1; i <= 4; i++)
			{
				Thread.Sleep(200);
				DateTime startDate = new(year, (i - 1) * 3 + 1, 1);
				DateTime endDate = startDate.AddMonths(3).AddDays(-1);
				int sumQuantityImport = _dataContext.Warehouse
					.Where(w => w.CreatedAt >= startDate && w.CreatedAt <= endDate)
					.Sum(qi => qi.QuantityImport);

				int stock = _dataContext.Warehouse
					.Where(w => w.CreatedAt >= startDate && w.CreatedAt <= endDate)
					.Sum(q => q.Quantity);

				dashboardDTO.WarehouseStatistics.Add(new WarehouseStatistic { Stock = stock, Import = sumQuantityImport });
			}

			return dashboardDTO;
		}

	}
}
