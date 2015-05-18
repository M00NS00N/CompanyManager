using System.Collections.Generic;
using CompanyManager.DatabaseAccessLayer.Context;

namespace CompanyManager.Models
{
    public class ProductPlanResultViewModel
    {
        public IEnumerable<ProductPlanResult> ProductPlanResults { get;set; }
        public IEnumerable<Node> Nodes { get; set; }
    }
    }