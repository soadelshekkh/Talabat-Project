using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Repositary.Configurations;

namespace Talabat.Repositary.DbContexts
{
    public class TalabatContext : DbContext
    {
        public TalabatContext(DbContextOptions<TalabatContext> options):base(options)
        {

        }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<ProductType> productTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Delivarymethod> DelivaryMethods { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new productConfigrations());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
