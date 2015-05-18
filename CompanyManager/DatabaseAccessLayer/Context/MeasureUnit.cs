namespace CompanyManager.DatabaseAccessLayer.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MeasureUnit")]
    public partial class MeasureUnit
    {
        public MeasureUnit()
        {
            Materials = new HashSet<Material>();
            Products = new HashSet<Product>();
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name="ѕолное наименование единицы измерени€")]
        public string MeasureUnitFullName { get; set; }

        [Required]
        [StringLength(5)]
        [Display(Name=" раткое наименование единицы измерени€")]
        public string MeasureUnitShortName { get; set; }

        public virtual ICollection<Material> Materials { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
