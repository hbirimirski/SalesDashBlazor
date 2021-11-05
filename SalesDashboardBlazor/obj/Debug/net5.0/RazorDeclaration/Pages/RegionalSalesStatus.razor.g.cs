// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace SalesDashboardBlazor.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Blazor\Progress Onboarding\Source\SalesDashBoardSource\SalesDashboardBlazor\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Blazor\Progress Onboarding\Source\SalesDashBoardSource\SalesDashboardBlazor\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Blazor\Progress Onboarding\Source\SalesDashBoardSource\SalesDashboardBlazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Blazor\Progress Onboarding\Source\SalesDashBoardSource\SalesDashboardBlazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Blazor\Progress Onboarding\Source\SalesDashBoardSource\SalesDashboardBlazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Blazor\Progress Onboarding\Source\SalesDashBoardSource\SalesDashboardBlazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Blazor\Progress Onboarding\Source\SalesDashBoardSource\SalesDashboardBlazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Blazor\Progress Onboarding\Source\SalesDashBoardSource\SalesDashboardBlazor\_Imports.razor"
using SalesDashboardBlazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Blazor\Progress Onboarding\Source\SalesDashBoardSource\SalesDashboardBlazor\_Imports.razor"
using SalesDashboardBlazor.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Blazor\Progress Onboarding\Source\SalesDashBoardSource\SalesDashboardBlazor\_Imports.razor"
using Telerik;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Blazor\Progress Onboarding\Source\SalesDashBoardSource\SalesDashboardBlazor\_Imports.razor"
using Telerik.Blazor.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Blazor\Progress Onboarding\Source\SalesDashBoardSource\SalesDashboardBlazor\Pages\RegionalSalesStatus.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Blazor\Progress Onboarding\Source\SalesDashBoardSource\SalesDashboardBlazor\Pages\RegionalSalesStatus.razor"
using SalesDashboardBlazor.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Blazor\Progress Onboarding\Source\SalesDashBoardSource\SalesDashboardBlazor\Pages\RegionalSalesStatus.razor"
using SalesDashboardBlazor.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Blazor\Progress Onboarding\Source\SalesDashBoardSource\SalesDashboardBlazor\Pages\RegionalSalesStatus.razor"
using Telerik.Blazor;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/RegionalSalesStatus")]
    public partial class RegionalSalesStatus : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 216 "C:\Blazor\Progress Onboarding\Source\SalesDashBoardSource\SalesDashboardBlazor\Pages\RegionalSalesStatus.razor"
       
    public class ModelDataLast
    {
        public double Series1 { get; set; }
        public double Series2 { get; set; }
        public double Series3 { get; set; }
    }

    public string[] Categories = new string[] { "2008", "2009", "2010", "2011" };

    public object[] AxisCrossingValue = new object[] { -10 };

    public List<ModelDataLast> DataLast = new List<ModelDataLast>()
{
        new ModelDataLast()
        {
            Series1 = 3.907,
            Series2 = 1.988,
            Series3 = -0.253
        },
        new ModelDataLast()
        {
            Series1 = 7.943,
            Series2 = 2.733,
            Series3 = 0.362
        },
        new ModelDataLast()
        {
            Series1 = 7.848,
            Series2 = 3.994,
            Series3 = -3.519
        },
        new ModelDataLast()
        {
            Series1 = 9.284,
            Series2 = 3.464,
            Series3 = 1.799
        }
    };


    DateValueViewModel revenuesViewModel = new DateValueViewModel();
    DateValueViewModel ordersViewModel = new DateValueViewModel();
    DateValueViewModel customersViewModel = new DateValueViewModel();
    MarketShareViewModel marketShareViewModel = new MarketShareViewModel();
    public TelerikNotification NotificationReference { get; set; }

    private DateTime startDate = new DateTime(1996, 1, 1);
    private DateTime endDate = new DateTime(1998, 1, 1);

    public void AddNotification()
    {
        NotificationReference.Show(new NotificationModel()
        {
            Text = "Auto Closable Notification",
            ThemeColor = "Primary"
        });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JS.InvokeAsync<string>("createMap");
        }
    }

    [JSInvokable]
    public static Task GetCountryCustomers(string selectedCountry)
    {
        string result = ProductsAndOrdersService.GetCompanies(selectedCountry);
        return Task.FromResult(result);
    }

    public async Task PopulateCharts()
    {
        var selectedCountry = await JS.InvokeAsync<string>("getSelectedCountry");
        revenuesViewModel = ProductsAndOrdersService.GetRevenue(startDate, endDate, selectedCountry);
        ordersViewModel = ProductsAndOrdersService.GetOrders(startDate, endDate, selectedCountry);
        customersViewModel = ProductsAndOrdersService.GetCustomers(startDate, endDate, selectedCountry);
        marketShareViewModel = ProductsAndOrdersService.GetMarketShare(startDate, endDate, selectedCountry);
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ProductsAndOrdersService ProductsAndOrdersService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JS { get; set; }
    }
}
#pragma warning restore 1591