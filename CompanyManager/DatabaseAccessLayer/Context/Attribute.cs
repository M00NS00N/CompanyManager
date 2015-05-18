using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace CompanyManager.DatabaseAccessLayer.Context
{
    [Table("Attribute")]
    public partial class Attribute
    {
        public Attribute()
        {
            Products = new HashSet<Product>();
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name="Признак")]
        public string AttributeName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
