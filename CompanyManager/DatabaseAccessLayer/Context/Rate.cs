namespace CompanyManager.DatabaseAccessLayer.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rate")]
    public partial class Rate
    {
        
        public Rate()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        [Required]
        [StringLength(128)]
        public string ProductId { get; set; }

        [Required]
        [StringLength(128)]
        public string MaterialId { get; set; }

        [Display(Name="Расход материала")]
        public float ConsumptionRate { get; set; }

        [Display(Name="Отходы материала")]
        public float WasteRate { get; set; }

        public virtual Material Material { get; set; }

        public virtual Product Product { get; set; }
    }
}
