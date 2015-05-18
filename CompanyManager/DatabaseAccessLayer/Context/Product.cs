namespace CompanyManager.DatabaseAccessLayer.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            Rates = new HashSet<Rate>();
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [Display(Name="Наименование продукта")]
        public string ProductName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="Код продукта")]
        public string ProductCode { get; set; }

        [Required]
        [StringLength(128)]
        public string KindId { get; set; }

        [Required]
        [StringLength(128)]
        public string TypeId { get; set; }

        [Required]
        [StringLength(128)]
        public string AttributeId { get; set; }

        [Required]
        [Display(Name="Обозначение")]
        public string Annotation { get; set; }

        [Display(Name="Количество")]
        public int Count { get; set; }

        //[Required]
        [StringLength(128)]
        public string ProductMeasureUnitId { get; set; }

        public virtual Attribute Attribute { get; set; }

        public virtual Kind Kind { get; set; }

        public virtual MeasureUnit ProductMeasureUnit { get; set; }

        public virtual Type Type { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }
    }
}
