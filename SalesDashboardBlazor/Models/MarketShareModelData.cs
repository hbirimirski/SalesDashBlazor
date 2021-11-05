using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDashboardBlazor.Models
{
    public class MarketShareModelData
    {
        public string Source { get; set; }
        public int Percentage { get; set; }
        public bool Explode { get; set; }
        public string SegmentColor { get; set; }
    }
}
