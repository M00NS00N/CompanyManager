using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManager.DatabaseAccessLayer.Context
{
    [Table("ProductPlanResult")]
    public class ProductPlanResult
    {
        public string Id { get; set; }
        public string MaterialId { get; set; }
        public Material Material { get; set; }
        public double Value { get; set; }

        public ProductPlanResult()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}