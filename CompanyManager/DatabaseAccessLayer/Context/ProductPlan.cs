using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManager.DatabaseAccessLayer.Context
{
    [Table("ProductPlan")]
    public class ProductPlan
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Count { get; set; }

        public ProductPlan()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}