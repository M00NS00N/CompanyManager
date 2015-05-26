using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CompanyManager.DatabaseAccessLayer.Context;

namespace CompanyManager.Models
{
    public class NodeViewModel
    {
        public string Id { get; set; }

        public string DestinationProductId { get; set; }
        public Product DestinationProduct { get; set; }
        
        public string InitialProductId { get; set; }
        public Product InitialProduct { get; set; }
                
        public string MainProductId { get; set; }
        public Product MainProduct { get; set; }
        
        public int Count { get; set; }

        //public IEnumerable<Product> MainProducts { get; set; }

        //public IEnumerable<Product> DestinationProducts { get; set; }

        //public IEnumerable<Product> InitialProducts { get; set; }
    }
}