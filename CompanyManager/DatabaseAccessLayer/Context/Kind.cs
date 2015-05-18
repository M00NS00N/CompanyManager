using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CompanyManager.DatabaseAccessLayer.Context
{
    
    [Table("Kind")]
    public partial class Kind
    {
        public Kind()
        {
            Products = new HashSet<Product>();
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name="Вид")]
        public string KindName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
