using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerProject.Data.Configurations
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //id alanı identity olsun ve 1er artsın
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            //name boş geçilemez uzunluk max200
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Stock).IsRequired();
            //18karakter virgülden sonra 2 karakter sakla boş geçilemez 
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.InnerBarcode).HasMaxLength(50);

        }
    }
}
