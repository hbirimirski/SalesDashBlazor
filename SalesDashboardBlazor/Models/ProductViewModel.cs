using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDashboardBlazor.Models
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public bool Discontinued { get; set; }
        public DateTime LastSupply { get; set; }
        public int UnitsOnOrder { get; set; }
        public string CategoryName { get; set; }

        public string QuantityPerUnit { get; set; }
        public int ReorderLevel { get; set; }
        public List<OrdersQty> OrdersQtys { get; set; }

        public object[] OrderDates { get; set; }
    }

    public class OrdersQty
    {
        public DateTime? OrderDate { get; set; }
        public int Quantity { get; set; }
    }
}
