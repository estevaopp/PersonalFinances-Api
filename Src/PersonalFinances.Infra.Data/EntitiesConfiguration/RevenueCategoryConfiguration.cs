using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinances.Domain.Entities;

namespace PersonalFinances.Infra.Data.EntitiesConfiguration
{
    public class RevenueCategoryConfiguration : IEntityTypeConfiguration<RevenueCategory>
    {
        public void Configure(EntityTypeBuilder<RevenueCategory> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).UseIdentityColumn();

            builder.Property(r => r.Name).IsRequired().HasMaxLength(30);

            builder.Property(r => r.Description).IsRequired().HasMaxLength(60);
        }
    }
}