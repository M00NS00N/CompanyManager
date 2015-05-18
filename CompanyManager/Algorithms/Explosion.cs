using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CompanyManager.DatabaseAccessLayer;
using CompanyManager.DatabaseAccessLayer.Context;

namespace CompanyManager.Algorithms
{
    public class Explosion
    {
        CompanyDatabaseContext db = new CompanyDatabaseContext();


        private IEnumerable<Node> GetAllWhere(string productCode)
        {
            
            //IList<Node> destinationProductList = UnitOfWork.Instance.NodeRepository.GetAll(x => x.DestinationProductId == destinationProductId, null, String.Empty).ToList();
            var nodes = db.Nodes.Include(n => n.DestinationProduct)
                                .Include(n => n.InitialProduct)
                                .Include(n => n.MainProduct)
                                .Where(n=>n.DestinationProduct.ProductCode == productCode);
            return nodes;
        }

        public void Calculate(Product node)
        {

            Method(GetAllWhere(node.ProductCode), node.Count);
            db.SaveChanges();
        }

        private void ClearTable()
        {
            var explosionNodes = db.ExplosionNodes.Include(e => e.MainProduct).Include(e => e.ProductComponent).ToList();
            foreach (var node in explosionNodes)
            {
                db.ExplosionNodes.Remove(node);
            }
            db.SaveChanges();
        }

        private void Method(IEnumerable<Node> table, int multiplier)
        {
            foreach (var node in table)
            {
                var list = GetAllWhere(node.InitialProduct.ProductCode);
                if (list.Count() > 0)
                {
                    Method(list, multiplier * node.Count);
                }
                else
                {
                    AddOrUpdate(new ExplosionNode
                    {
                        MainProductId = node.MainProductId,
                        ProductComponentId = node.InitialProductId,
                        Count = multiplier * node.Count
                    });
                }
            }
        }

        private void AddOrUpdate(ExplosionNode node)
        {
            node.ProductComponent = db.Products.Find(node.ProductComponentId);
            var sameNode =  db.ExplosionNodes.Include(x=>x.MainProduct)
                                            .Include(x=>x.ProductComponent)
                                            .FirstOrDefault(x=>x.ProductComponent.ProductCode == node.ProductComponent.ProductCode);
            node.ProductComponent = null;
            if (sameNode == null)
            {
                UnitOfWork.Instance.ExplosionRepository.Add(node);
                //db.ExplosionNodes.Add(node);
            }
            else
            {
                sameNode.Count += node.Count;
                //sameNode.MainProduct = null;
                //sameNode.ProductComponent = null;
                db.Entry(sameNode).State = EntityState.Modified;
            //    db.SaveChanges();
                //UnitOfWork.Instance.ExplosionRepository.Update(obj);
            }
            //
        }
    }
}