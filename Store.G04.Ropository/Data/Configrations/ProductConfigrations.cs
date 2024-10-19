﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Ropository.Data.Configrations
{
    public class ProductConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
            builder.Property(P => P.PictureUrl).IsRequired(true);

            builder.Property(P => P.Price).HasColumnType("decimal(18.2)");

            builder.HasOne(P => P.Brand)
                   .WithMany()
                   .HasForeignKey(p => p.BrandId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(P => P.Type)
                   .WithMany()
                   .HasForeignKey(P=>P.TypeId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Property(P => P.TypeId).IsRequired(false);
            builder.Property(P => P.BrandId).IsRequired(false);
        }
    }
}
