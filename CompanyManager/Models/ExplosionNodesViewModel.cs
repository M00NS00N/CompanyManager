using System.Collections.Generic;
using System.EnterpriseServices;
using CompanyManager.DatabaseAccessLayer.Context;

namespace CompanyManager.Models
{
    public class ExplosionNodesViewModel
    {
        public IEnumerable<ExplosionNode> ExplosionNodes { get; set; }
        public IEnumerable<Node> Nodes { get; set; }
    }
}