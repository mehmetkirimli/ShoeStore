﻿using Microsoft.EntityFrameworkCore;
using ShoeStore.Entities;

namespace ShoeStore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);  // Kategori silinince ürünleri de sil

            // Product ve OrderItem arasındaki ilişki
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)  // OrderItem, bir Product'a bağlı
                .WithMany(p => p.OrderItems)  // Product, birden çok OrderItem'a sahip olabilir
                .HasForeignKey(oi => oi.ProductId)  // OrderItem içindeki ProductId, Foreign Key'dir
                .OnDelete(DeleteBehavior.Cascade);  // Ürün silinirse ilgili OrderItem'lar da silinsin

            // Order ve User arasındaki ilişki
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)  // Her sipariş bir kullanıcıya ait
                .WithMany()  // Bir kullanıcı birden fazla siparişe sahip olabilir
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);  // Kullanıcı silindiğinde siparişler silinmez

            // Order ve Payment arasındaki ilişki (Payment nullable)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)  // Siparişin bir ödeme kaydı olabilir
                .WithOne(p => p.Order)  // Ödeme yalnızca bir siparişle ilişkili olabilir
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.SetNull);  // Ödeme silindiğinde siparişin ödeme bilgisi null olur

            // Order ve OrderItem arasındaki ilişki
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)  // Sipariş kalemi bir siparişe aittir
                .WithMany(o => o.OrderItems)  // Sipariş birden fazla sipariş kalemi içerir
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);  // Sipariş silindiğinde sipariş kalemleri de silinir

            base.OnModelCreating(modelBuilder);
        }
    }
}
