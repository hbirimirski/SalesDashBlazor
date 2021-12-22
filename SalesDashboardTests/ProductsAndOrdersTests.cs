using Bunit;
using Microsoft.Extensions.DependencyInjection;
using SalesDashboardBlazor.Models;
using SalesDashboardBlazor.Pages;
using SalesDashboardBlazor.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telerik.Blazor.Components;
using Telerik.JustMock;
using Xunit;

namespace SalesDashboardTests
{
    public class ProductsAndOrdersTests
    {
        [Fact]
        public void Test_Products()
        {
            var ctx = new TestContext();

            var orders = new List<OrderViewModel>() { new OrderViewModel {  ContactName = string.Empty, CustomerID = "VICTE", CustomerName = "Mary Saveley", EmployeeID = 3, EmployeeName = "Janet Leverling",
            Freight = null, OrderDate = DateTime.Now, OrderID = 10251, ShipAddress = "2, rue du Commerce", ShipCity = "Lyon", ShipCountry = "France", ShipName = "Victuailles en stock", ShipPostalCode = "69004",
            ShippedDate = DateTime.Now.AddDays(2), ShipperName = "Speedy Express", ShipVia = 1 } };

            var productsServiceMock = Mock.Create<IProductsAndOrdersService>();
            Mock.Arrange(() => productsServiceMock.GetOrders())
                .Returns(Task.FromResult<List<OrderViewModel>>(orders));

            ctx.Services.AddSingleton<IProductsAndOrdersService>(productsServiceMock);

            ctx.JSInterop.Mode = JSRuntimeMode.Loose;
            ctx.Services.AddTelerikBlazor();
            var rootComponentMock = Mock.Create<TelerikRootComponent>();


            var cut = ctx.RenderComponent<ProductsAndOrders>(parameters => parameters
               .AddCascadingValue<TelerikRootComponent>(rootComponentMock));

            var masterRowSelector = cut.FindAll("tr.k-master-row");

            Assert.Collection(masterRowSelector,
                  row => Assert.Contains("10251", row.InnerHtml));

        }
    }

}