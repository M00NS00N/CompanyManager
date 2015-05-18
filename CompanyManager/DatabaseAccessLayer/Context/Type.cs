namespace CompanyManager.DatabaseAccessLayer.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Type")]
    public partial class Type
    {
        public Type()
        {
            Products = new HashSet<Product>();
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name="“ËÔ")]
        public string TypeName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
