using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CompanyManager.DatabaseAccessLayer.Context
{
    [Table("ExplosionNode")]
    public partial class ExplosionNode
    {
        public ExplosionNode()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        //[Required]
        [StringLength(128)]
        [Display(Name="Продукт")]
        public string MainProductId { get; set; }
        public Product MainProduct { get; set; }

        //[Required]
        [StringLength(128)]
        [Display(Name="Деталь")]
        public string ProductComponentId { get; set; }
        public Product ProductComponent { get; set; }

        [Display(Name="Количество")]
        public int Count { get; set; }
    }
}
