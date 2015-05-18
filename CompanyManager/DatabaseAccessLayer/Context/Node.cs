namespace CompanyManager.DatabaseAccessLayer.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Node")]
    public partial class Node
    {
        public string Id { get; set; }

        public Node()
        {
            this.Id = Guid.NewGuid().ToString(); 
        }

        //[Required]
        [StringLength(128)]
        [Display(Name="����")]
        public string DestinationProductId { get; set; }
        public Product DestinationProduct { get; set; }

        //[Required]
        [StringLength(128)]
        [Display(Name="���")]
        public string InitialProductId { get; set; }
        public Product InitialProduct { get; set; }
        
        //[Required]
        [StringLength(128)]
        [Display(Name="�������")]
        public string MainProductId { get; set; }
        public Product MainProduct { get; set; }

        [Display(Name="����������")]
        public int Count { get; set; }
    }
}
