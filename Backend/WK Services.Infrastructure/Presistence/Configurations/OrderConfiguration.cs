using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WK_Services.Domain.Entities;

namespace WK_Services.Infrastructure.Presistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasIndex(o => o.OrderNumber).IsUnique();
            builder.HasOne(o => o.CreatedByContact)
               .WithMany(c => c.Orders)
               .HasForeignKey(o => o.CreatedByContactId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
