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

		public DashboardDTO GetDashboardInfo(int year)
		{
			DashboardDTO dashboardDTO = new();
			dashboardDTO.TotalProducts = _dataContext.Products.Count();
			dashboardDTO.TotalImportNotes = _dataContext.ImportNotes.Count();
			dashboardDTO.TotalUsers = _dataContext.Users.Count();

			if (year == 0)
			{
				year = DateTime.UtcNow.Year;
			}

			for (int i = 1; i <= 4; i++)
			{
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
