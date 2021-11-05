using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDashboardBlazor.Models
{
    public class EmployeeAverageSalesViewModel
    {
        public int EmployeeID { get; set; }
        public decimal EmployeeSales { get; set; }
        public decimal TotalSales { get; set; }
        public DateTime Date { get; set; }
    }
}
