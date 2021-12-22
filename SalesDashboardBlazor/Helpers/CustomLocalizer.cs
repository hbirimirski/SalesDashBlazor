using SalesDashboardBlazor.Resources;
using Telerik.Blazor.Services;
using TelerikBlazorDemos.Resources;

namespace SalesDashboardBlazor.Helpers
{
 
    public class CustomLocalizer : ITelerikStringLocalizer
    {
        // this is the indexer you must implement
        public string this[string name]
        {
            get
            {
                return GetStringFromResource(name);
            }
        }

        public string GetStringFromResource(string key)
        {
            return TelerikMessages.ResourceManager.GetString(key, TelerikMessages.Culture);
        }
    }
}
