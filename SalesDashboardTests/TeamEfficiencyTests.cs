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
    public class TeamEfficiencyTests
    {
        [Fact]
        public void Test_TeamEff_Init()
        {
            var ctx = new TestContext();

            var employees = new List<EmployeeViewModel>() { new EmployeeViewModel { Address = "Test address", City = "Sofia", Country = "Bulgaria", EmployeeID = 1, 
            EmployeeName = "Danail", FirstName = "Danail", HomePhone = "53453453", Notes = "Note",  LastName = "Petrov", Title = "Mr.", Selected = ""} };

            var productsServiceMock = Mock.Create<IProductsAndOrdersService>();
            Mock.Arrange(() => productsServiceMock.GetEmployeesList())
                .Returns(Task.FromResult<List<EmployeeViewModel>>(employees));

            ctx.Services.AddSingleton<IProductsAndOrdersService>(productsServiceMock);

            ctx.JSInterop.Mode = JSRuntimeMode.Loose;
            ctx.Services.AddTelerikBlazor();
            var rootComponentMock = Mock.Create<TelerikRootComponent>();

            var cut = ctx.RenderComponent<TeamEfficiency>(parameters => parameters
               .AddCascadingValue<TelerikRootComponent>(rootComponentMock));

            cut.Find("h3").MarkupMatches("<h3>TEAM MEMBERS</h3>");
        }
    }

}