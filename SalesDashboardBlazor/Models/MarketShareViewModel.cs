using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDashboardBlazor.Models
{
    public class MarketShareViewModel
    {
        public string All { get; set; }
        public string Country { get; set; }
        public string CountryPercent { get; set; }

        public List<MarketShareModelData> Data { get; set; }
    }
}
