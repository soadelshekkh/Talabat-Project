using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repositary.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, NP => NP.WithOwner());
            builder.Property(o => o.OrderStatus).HasConversion(OStatus => OStatus.ToString(),OStatus =>(OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus));
            builder.HasMany(o => o.orderItem).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
