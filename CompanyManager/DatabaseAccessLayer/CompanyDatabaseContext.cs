using System.Web.WebPages;

namespace CompanyManager.DatabaseAccessLayer.Context
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CompanyDatabaseContext : DbContext
    {
        public CompanyDatabaseContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<ExplosionNode> ExplosionNodes { get; set; }
        public virtual DbSet<Kind> Kinds { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<MeasureUnit> MeasureUnits { get; set; }
        public virtual DbSet<Node> Nodes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<ProductPlan> ProductPlans { get; set; }
        public virtual DbSet<ProductPlanResult> ProductPlanResults { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
