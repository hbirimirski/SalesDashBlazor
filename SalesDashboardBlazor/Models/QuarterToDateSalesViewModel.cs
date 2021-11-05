using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDashboardBlazor.Models
{
    public class QuarterToDateSalesViewModel
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Current { get; set; }
        public decimal Target { get; set; }
    }
}
