using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CompanyManager.DatabaseAccessLayer.Context
{
    [Table("Material")]
    public partial class Material
    {
        public Material()
        {
            Rates = new HashSet<Rate>();
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name="Наменование материала")]
        public string MaterialName { get; set; }

        //[Required]
        [StringLength(128)]
        public string MeasureUnitId { get; set; }

        [Required]
        [Display(Name="Код материала")]
        public string Code { get; set; }

        public virtual MeasureUnit MeasureUnit { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }
    }
}
