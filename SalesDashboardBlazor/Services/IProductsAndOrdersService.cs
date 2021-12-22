using SalesDashboardBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDashboardBlazor.Services
{
    public interface IProductsAndOrdersService
    {
        string GetCompanies(string country);
        DateValueViewModel GetCustomers(DateTime startDate, DateTime endDate, string selectedCountry);
        List<EmployeeAverageSalesViewModel> GetEmployeeAverageSales(int employeeID, DateTime fromDate, DateTime toDate);
        List<QuarterToDateSalesViewModel> GetEmployeeQuarterSales(int employeeID, DateTime toDate);
        Task<List<EmployeeViewModel>> GetEmployeesList();
        MarketShareViewModel GetMarketShare(DateTime startDate, DateTime endDate, string selectedCountry);
        IQueryable<OrderDetailsViewModel> GetOrderDetails(int orderId);
        Task<List<OrderViewModel>> GetOrders();
        DateValueViewModel GetOrders(DateTime startDate, DateTime endDate, string selectedCountry);
        ProductViewModel GetProductInformation(int productId);
        DateValueViewModel GetRevenue(DateTime startDate, DateTime endDate, string selectedCountry);
    }
}