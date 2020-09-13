using Microsoft.EntityFrameworkCore;
using NLayerProject.Core.Models;
using NLayerProject.Data.Configurations;
using NLayerProject.Data.Seeds;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerProject.Data
{
   public class AppDbContext:DbContext
    {
        //optionsu startup ta contexti dolduruyor olacağız.
        //dbye karşılık gelir
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        //dbset tablolara karşılık gelir
        public DbSet<Category> Categories{ get; set; }
        public DbSet <Product> products { get; set; }
        //veri tabanında tablolar oluşmadan önce çalışacak method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //identfy olacakmı kolon uzunları ne olacak gibi bilgileri belirticez
            //burdanda kodlayabiliriz aa biz configuration dosylarına ayırdık burada onları çağırdık.
            //modelBuilder.Entity<Product>().Property(x => x.Id).UseIdentityColumn();
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            //tablo içindeki default dataları yine burada tablo içine basabilirdik ama Seed clası içinden alacağız
            modelBuilder.ApplyConfiguration(new ProductSeed(new int[] { 1, 2 }));
            modelBuilder.ApplyConfiguration(new CategorySeed(new int[] { 1, 2}));

        }
    }
}
