﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Online_Store.Models.Entities;
using Online_Store.Models;

namespace Online_Store.Data
{
    public class ApplicationDbContext: IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Здесь можно настроить отношения между таблицами

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasMany(c => c.Items)
                      .WithOne()
                      .HasForeignKey(i => i.CartId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.Property(c => c.CreatedAt)
              .HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.HasOne<Product>()
                      .WithMany()
                      .HasForeignKey(i => i.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
