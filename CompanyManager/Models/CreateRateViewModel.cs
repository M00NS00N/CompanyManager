using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CompanyManager.DatabaseAccessLayer.Context;
namespace CompanyManager.Models
{
    public class CreateRateViewModel
    {
        public RateViewModel RateViewModel { get; set; }

        public IEnumerable<Material> Materials { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}