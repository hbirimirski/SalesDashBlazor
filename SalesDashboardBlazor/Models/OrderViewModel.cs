using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDashboardBlazor.Models
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }

        public string CustomerID { get; set; }

        public string ContactName { get; set; }

        public decimal? Freight { get; set; }

        public string ShipAddress { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public string ShipCountry { get; set; }

        public string ShipCity { get; set; }

        public string ShipName { get; set; }

        public int? EmployeeID { get; set; }

        public int? ShipVia { get; set; }

        public string ShipPostalCode { get; set; }

        public string EmployeeName { get; set; }
        public string CustomerName { get; set; }
        public string ShipperName { get; set; }

        //public virtual Employee Employee { get; set; }
        //public virtual Customer Customer { get; set; }
        //public virtual Shipper Shipper { get; set; }
    }
}
