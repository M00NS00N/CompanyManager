using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyManager.Models
{
    public class RateViewModel
    {
        public string Id { get; set; }

        public string ProductId { get; set; }

        public string MaterialId { get; set; }
        
        public float ConsumptionRate { get; set; }
        
        public float WasteRate { get; set; }
    }
}