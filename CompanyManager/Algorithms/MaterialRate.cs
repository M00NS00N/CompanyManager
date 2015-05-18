using System.Collections.Generic;
using System.Linq;
using CompanyManager.DatabaseAccessLayer.Context;

namespace CompanyManager.Algorithms
{
    public class MaterialRate
    {
        CompanyDatabaseContext context = new CompanyDatabaseContext();


        public List<ProductPlanResult> Calculate(string id)
        {
            var explosionAlg = new Explosion();
            List<ProductPlanResult> result = new List<ProductPlanResult>();
            Product product = context.Products.Find(id);
            if (product != null)
            {
                explosionAlg.Calculate(product);
                var explosionNodes = context.ExplosionNodes.ToList();
                List<Rate> rates = explosionNodes.Select(node => context.Rates.FirstOrDefault(x => x.ProductId == node.ProductComponentId))
                    .Where(materialRate => materialRate != null).ToList();
                
                foreach (var rate in rates)
                {
                    var material = context.Materials.FirstOrDefault(x => x.Id == rate.MaterialId);
                    if (material != null)
                    {
                        var expNode = explosionNodes.FirstOrDefault(x => x.ProductComponentId == rate.ProductId);
                        if (expNode != null)
                        {
                            var resultU = result.FirstOrDefault(x => x.MaterialId == material.Id);
                            if (resultU == null)
                            {
                                result.Add(new ProductPlanResult()
                                {
                                    MaterialId = material.Id,
                                    Value = (rate.ConsumptionRate)*expNode.Count
                                });
                            }
                            else
                            {
                                result.Remove(resultU);
                                resultU.Value += (rate.ConsumptionRate)*expNode.Count;
                                result.Add(resultU);
                            }
                        }
                    }
                }
                
            }
            var alddata = context.ProductPlanResults.ToList();
            foreach (var x in alddata) 
                context.ProductPlanResults.Remove(x);
            foreach (var x in result) 
                context.ProductPlanResults.Add(x);
            context.SaveChanges();
            return result;
        }
    }
}