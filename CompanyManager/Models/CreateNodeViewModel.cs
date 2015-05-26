using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyManager.Models
{
    public class CreateNodeViewModel
    {
        public NodeViewModel Node { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}