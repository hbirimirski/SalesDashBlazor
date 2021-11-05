using SalesDashboardBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDashboardBlazor.Services
{
    public class ProductsAndOrdersService
    {
        public static IQueryable<OrderViewModel> GetOrders()
        {
            DataAccessLib.NorthwindContext dbContext = new DataAccessLib.NorthwindContext();

            var orders = dbContext.Orders.Select(order => new OrderViewModel
            {
                CustomerID = order.CustomerId,
                OrderID = order.OrderId,
                EmployeeID = order.EmployeeId,
                EmployeeName = order.Employee.FirstName + " " + order.Employee.LastName,
                CustomerName = order.Customer.ContactName,
                //ShipperName = order.ShipName,
                ShipperName = dbContext.Shippers.Where(x => x.ShipperId == order.ShipVia).Select(x => x.CompanyName).FirstOrDefault(),
                OrderDate = order.OrderDate,
                ShipCountry = order.ShipCountry,
                ShipVia = order.ShipVia,
                ShippedDate = order.ShippedDate,
                ShipName = order.ShipName,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity,
                ShipPostalCode = order.ShipPostalCode
            });

            return orders;
        }

        public static IQueryable<OrderDetailsViewModel> GetOrderDetails(int orderId)
        {
            DataAccessLib.NorthwindContext dbContext = new DataAccessLib.NorthwindContext();

            var orderDetails = dbContext.OrderDetails.Where(x => x.OrderId == orderId).Select(orderDetails => new OrderDetailsViewModel
            {
                OrderId = orderDetails.OrderId,
                ProductId = orderDetails.ProductId,
                ProductName = orderDetails.Product.ProductName,
                Price = orderDetails.UnitPrice,
                Discount = orderDetails.Discount,
                Quantity = orderDetails.Quantity,
                Total = ((double)orderDetails.UnitPrice * orderDetails.Quantity),
                Category = orderDetails.Product.Category.CategoryName
            });

            return orderDetails;
        }

        public static ProductViewModel GetProductInformation(int productId)
        {
            using (DataAccessLib.NorthwindContext dbContext = new DataAccessLib.NorthwindContext())
            {
                ProductViewModel productViewModel = dbContext.Products.Where(x => x.ProductId == productId).Select(product => new ProductViewModel
                {
                    ProductID = product.ProductId,
                    ProductName = product.ProductName,
                    CategoryName = product.Category.CategoryName,
                    UnitsInStock = (int)product.UnitsInStock,
                    UnitsOnOrder = (int)product.UnitsOnOrder,
                    ReorderLevel = (int)product.ReorderLevel,
                    QuantityPerUnit = product.QuantityPerUnit
                }).FirstOrDefault();

                List<OrdersQty> lOrdersQty = (from o in dbContext.Orders
                                              join od in dbContext.OrderDetails on o.OrderId equals od.OrderId
                                              where od.ProductId == productId
                                              select new OrdersQty
                                              {
                                                  OrderDate = o.OrderDate,
                                                  Quantity = od.Quantity
                                              }).ToList();

                productViewModel.OrdersQtys = lOrdersQty;
                productViewModel.OrderDates = productViewModel.OrdersQtys.Select(x => x.OrderDate.ToString()).ToArray();

                return productViewModel;
            }
        }

        public static string GetCompanies(string country)
        {
            using (DataAccessLib.NorthwindContext dbContext = new DataAccessLib.NorthwindContext())
            {
                var q = dbContext.Customers.Where(x => x.Country == country).Select(x => x.CompanyName).ToList();
                string result = string.Join(",", q);
                return result;
            }
        }

        public DateValueViewModel GetRevenue(DateTime startDate, DateTime endDate, string selectedCountry)
        {
            var country = selectedCountry;

            using (DataAccessLib.NorthwindContext dbContext = new DataAccessLib.NorthwindContext())
            {

                var q1 = (from o in dbContext.Orders
                          join od in dbContext.OrderDetails on o.OrderId equals od.OrderId
                          where o.OrderDate >= startDate && o.OrderDate <= endDate && o.ShipCountry == country
                          select new
                          {
                              OrderID = o.OrderId,
                              EmployeeID = o.EmployeeId,
                              Date = o.OrderDate,
                              Sales = od.Quantity * od.UnitPrice
                          }).AsEnumerable();
                var q2 = (from allSales in q1
                          group allSales by allSales.OrderID into g
                          select new
                          {
                              OrderID = g.Key,
                              EmployeeID = g.FirstOrDefault().EmployeeID,
                              Sales = g.Sum(x => x.Sales),
                              Date = new DateTime(g.FirstOrDefault().Date.Value.Year, g.FirstOrDefault().Date.Value.Month, 1),
                          });
                var q3 = (from groupedSales in q2
                          group groupedSales by new { groupedSales.EmployeeID, groupedSales.Date } into gs
                          select new
                          {
                              EmployeeID = gs.FirstOrDefault().EmployeeID,
                              Date = gs.Key.Date,
                              Sales = gs.Sum(x => x.Sales)
                          });

                var final = (from totalSales in q3
                             group totalSales by totalSales.Date into gs
                             select new
                             {
                                 Date = gs.Key,
                                 Value = gs.Sum(x => x.Sales)
                             }).ToList();

                DateValueViewModel result = new DateValueViewModel();
                result.Date = final.Select(x => x.Date.ToString()).ToArray();
                result.Value = final.Select(x => (object)x.Value).ToList();

                return result;
            }
        }

        public DateValueViewModel GetOrders(DateTime startDate, DateTime endDate, string selectedCountry)
        {
            using (DataAccessLib.NorthwindContext dbContext = new DataAccessLib.NorthwindContext())
            {
                var data = dbContext.Orders.Where(o => o.OrderDate >= startDate
                                                                && o.OrderDate <= endDate
                                                                && o.ShipCountry == selectedCountry);
                var final = from o in data
                            group o by o.OrderDate into g
                            select new
                            {
                                Date = g.Key,
                                Value = g.Count()
                            };

                DateValueViewModel result = new DateValueViewModel();
                result.Date = final.Select(x => x.Date.ToString()).ToArray();
                result.Value = final.Select(x => (object)x.Value).ToList();
                return result;
            }
        }

        public DateValueViewModel GetCustomers(DateTime startDate, DateTime endDate, string selectedCountry)
        {
            using (DataAccessLib.NorthwindContext dbContext = new DataAccessLib.NorthwindContext())
            {
                var q = (from allCustomers in
                                  (from o in dbContext.Orders
                                   join od in dbContext.OrderDetails on o.OrderId equals od.OrderId
                                   where o.OrderDate >= startDate && o.OrderDate <= endDate && o.ShipCountry == selectedCountry
                                   select new
                                   {
                                       CustomerID = o.CustomerId,
                                       Date = o.OrderDate
                                   }).AsEnumerable()
                         group allCustomers by new { Date = new DateTime(allCustomers.Date.Value.Year, allCustomers.Date.Value.Month, 1) } into g
                         select new
                         {
                             Date = g.Key.Date,
                             Value = g.GroupBy(x => x.CustomerID).Count()
                         }
                );

                DateValueViewModel result = new DateValueViewModel();
                result.Date = q.Select(x => x.Date.ToString()).ToArray();
                result.Value = q.Select(x => (object)x.Value).ToList();
                return result;
            }
        }

        public MarketShareViewModel GetMarketShare(DateTime startDate, DateTime endDate, string selectedCountry)
        {
            using (DataAccessLib.NorthwindContext dbContext = new DataAccessLib.NorthwindContext())
            {
                var allSales = (from o in dbContext.Orders
                                join od in dbContext.OrderDetails on o.OrderId equals od.OrderId
                                where o.OrderDate >= startDate && o.OrderDate <= endDate
                                select new
                                {
                                    Country = o.ShipCountry,
                                    Sales = od.Quantity * od.UnitPrice
                                }).AsQueryable();

                int salesCount = allSales.Where(w => w.Country == selectedCountry).Count();

                MarketShareViewModel result = new MarketShareViewModel();

                result.All = allSales.Sum(x => x.Sales).ToString("0.00") ?? "0" + " %";
                result.Country = (salesCount != 0) ? allSales.Where(w => w.Country == selectedCountry).Sum(s => s.Sales).ToString("0.00") : "0";
                result.CountryPercent = (Convert.ToDecimal(result.Country) * 100 / Convert.ToDecimal(result.All)).ToString("0.00");

                result.Data = new List<MarketShareModelData>();
                MarketShareModelData countryData = new MarketShareModelData();
                countryData.Explode = false;
                countryData.Source = selectedCountry;
                countryData.Percentage = Convert.ToInt32(Convert.ToDecimal(result.CountryPercent));
                countryData.SegmentColor = "#59AF6B";
                result.Data.Add(countryData);

                MarketShareModelData dataAll = new MarketShareModelData();
                dataAll.Explode = false;
                dataAll.Source = "All";
                dataAll.Percentage = 100 - countryData.Percentage;
                dataAll.SegmentColor = "#A5C933";
                result.Data.Add(dataAll);

                result.CountryPercent = result.CountryPercent + " %";
                result.Country = "$ " + result.Country;

                return result;
            }
        }

        public List<EmployeeViewModel> GetEmployeesList()
        {
            using (DataAccessLib.NorthwindContext dbContext = new DataAccessLib.NorthwindContext())
            {
                var employees = dbContext.Employees.Select(e => new EmployeeViewModel
                {
                    EmployeeID = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    EmployeeName = e.FirstName + " " + e.LastName,
                    Notes = e.Notes,
                    Title = e.Title,
                    HomePhone = e.HomePhone
                }).OrderBy(e => e.FirstName).ToList();

                employees.FirstOrDefault().Selected = "selected";
                return employees;
            }
        }

        public List<QuarterToDateSalesViewModel> GetEmployeeQuarterSales(int employeeID, DateTime toDate)
        {
            DateTime startDate = toDate.AddMonths(-3);
            using (DataAccessLib.NorthwindContext dbContext = new DataAccessLib.NorthwindContext())
            {
                var sales = dbContext.Orders.Where(w => w.EmployeeId == employeeID)
                                     .Join(dbContext.OrderDetails, orders => orders.OrderId, orderDetails => orderDetails.OrderId,
                                     (orders, orderDetails) => new
                                     {
                                         Order = orders,
                                         OrderDetails = orderDetails
                                     })
                                     .Where(d => d.Order.OrderDate >= startDate && d.Order.OrderDate <= toDate).ToList()
                                     .Select(o => new QuarterToDateSalesViewModel
                                     {
                                         Current = (o.OrderDetails.Quantity * o.OrderDetails.UnitPrice) - (o.OrderDetails.Quantity * o.OrderDetails.UnitPrice * (decimal)o.OrderDetails.Discount)
                                     });

                var result = new List<QuarterToDateSalesViewModel>() {
                     new QuarterToDateSalesViewModel
                     {
                         Current = sales.Sum(s=>s.Current), Target = 15000, OrderDate = toDate
                     }
                };

                return result;
            }
        }

        public List<EmployeeAverageSalesViewModel> GetEmployeeAverageSales(int employeeID, DateTime fromDate, DateTime toDate)
        {
            using (DataAccessLib.NorthwindContext dbContext = new DataAccessLib.NorthwindContext())
            {
                List<EmployeeAverageSalesViewModel> result = (from allSales in
                (from o in dbContext.Orders
                 join od in dbContext.OrderDetails on o.OrderId equals od.OrderId
                 where
                o.EmployeeId == employeeID &&
                o.OrderDate >= fromDate && o.OrderDate <= toDate
                 select new
                 {
                     EmployeeID = o.EmployeeId,
                     Date = o.OrderDate,
                     Sales = od.Quantity * od.UnitPrice
                 }).ToList()
                                                              group allSales by new DateTime(allSales.Date.Value.Year, allSales.Date.Value.Month, 1) into g
                                                              select new EmployeeAverageSalesViewModel
                                                              {
                                                                  EmployeeID = (int)g.FirstOrDefault().EmployeeID,
                                                                  EmployeeSales = g.Sum(x => x.Sales),
                                                                  Date = g.Key,
                                                              }).ToList();

                List<EmployeeAverageSalesViewModel> resultTeam = (from allSales in
                (from o in dbContext.Orders
                 join od in dbContext.OrderDetails on o.OrderId equals od.OrderId
                 where
                                o.OrderDate >= fromDate && o.OrderDate <= toDate
                 select new
                 {
                     EmployeeID = o.EmployeeId,
                     Date = o.OrderDate,
                     Sales = od.Quantity * od.UnitPrice
                 }).ToList()
                                                                  group allSales by new DateTime(allSales.Date.Value.Year, allSales.Date.Value.Month, 1) into g
                                                                  select new EmployeeAverageSalesViewModel
                                                                  {
                                                                      EmployeeID = (int)g.FirstOrDefault().EmployeeID,
                                                                      EmployeeSales = g.Sum(x => x.Sales),
                                                                      Date = g.Key,
                                                                  }).ToList();

                foreach (var item in result)
                {
                    item.TotalSales = resultTeam.Where(x => x.Date == item.Date && x.EmployeeID != item.EmployeeID).Sum(x => x.EmployeeSales);
                }

                return result;
            }

        }
    }
}
