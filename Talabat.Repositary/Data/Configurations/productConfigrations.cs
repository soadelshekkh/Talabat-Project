using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repositary.Configurations
{
    public class productConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(60);
            builder.Property(p=> p .PictureUrl).IsRequired(true);
            builder.Property(p => p.Description).IsRequired(true);
            //builder.Property(p => p.Price).IsRequired(true).HasColumnType("decimal(18,2)");
            builder.HasOne(p => p.Productbrand).WithMany().HasForeignKey(p => p.ProductBrandId);
            builder.HasOne(p => p.productType).WithMany().HasForeignKey(p => p.ProductTypeId);

        }
    }
}
