using System;
using System.Collections.Generic;
using System.Linq;
using CompanyManager.DatabaseAccessLayer.Context;
using CompanyManager.DatabaseAccessLayer.Repositories;
using CompanyManager.DatabaseAccessLayer.Repositories.Interfaces;
using Attribute = CompanyManager.DatabaseAccessLayer.Context.Attribute;
using Type = CompanyManager.DatabaseAccessLayer.Context.Type;

namespace CompanyManager.DatabaseAccessLayer
{
    public class UnitOfWork:IDisposable
    {
        private CompanyDatabaseContext context = new CompanyDatabaseContext();
        private IGenericRepository<Attribute> attributeRepository;
        private IGenericRepository<Kind> kindRepository;
        private IGenericRepository<Material> materialRepository;
        private IGenericRepository<MeasureUnit> measureUnitRepository;
        private IGenericRepository<Product> productRepository;
        private IGenericRepository<Rate> rateRepository;
        private IGenericRepository<Type> typeRepository;
        private IGenericRepository<ExplosionNode> explosionRepository;
        private IGenericRepository<Node> nodeRepository;

        public static UnitOfWork Instance = new UnitOfWork();

        private UnitOfWork()
        {

        }

        public IGenericRepository<Node> NodeRepository
        {
            get
            {
                if(nodeRepository==null)
                {
                    nodeRepository = new GenericRepository<Node>(context);
                }

                return nodeRepository;
            }
        }

        public IGenericRepository<ExplosionNode> ExplosionRepository
        {
            get
            {
                if(explosionRepository==null)
                {
                    explosionRepository = new GenericRepository<ExplosionNode>(context);
                }

                return explosionRepository;
            }
        }

        public IGenericRepository<Attribute> AttributeRepository
        {
            get
            {
                if(attributeRepository == null)
                {
                    attributeRepository = new GenericRepository<Attribute>(context);
                }

                return attributeRepository;
            }
        }

        public IGenericRepository<Kind> KindRepository
        {
            get
            {
                if(kindRepository == null)
                {
                    kindRepository = new GenericRepository<Kind>(context);
                }

                return kindRepository;
            }
        }
    
        public IGenericRepository<Material> MaterialRepository
        {
            get
            {
                if(materialRepository == null)
                {
                    materialRepository = new GenericRepository<Material>(context);
                }

                return materialRepository;
            }
        }

        public IGenericRepository<MeasureUnit> MeasureUnitRepository
        {
            get
            {
                if(measureUnitRepository == null)
                {
                    measureUnitRepository = new GenericRepository<MeasureUnit>(context);
                }
                return measureUnitRepository;
            }
        }

        public IGenericRepository<Product> ProductRepository
        {
            get
            {
                if(productRepository == null)
                {
                    productRepository = new GenericRepository<Product>(context);
                }

                return productRepository;
            }
        }

        public IGenericRepository<Rate> RateRepository
        {
            get
            {
                if(rateRepository == null)
                {
                    rateRepository = new GenericRepository<Rate>(context);
                }

                return rateRepository;
            }
        }

        public IGenericRepository<Type> TypeRepository
        {
            get
            {
                if(typeRepository == null)
                {
                    typeRepository = new GenericRepository<Type>(context);
                }
                return typeRepository;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}