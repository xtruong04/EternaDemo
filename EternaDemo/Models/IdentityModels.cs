using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EternaDemo.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Gemstone> Gemstones { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Metal> Metals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ngắt cascade để tránh multiple cascade paths

            // Order -> User
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .WillCascadeOnDelete(false);

            // Address -> User
            modelBuilder.Entity<Address>()
                .HasRequired(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);

            // Order -> Address
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId)
                .WillCascadeOnDelete(false);

            // OrderItem -> Order (cho phép cascade khi xóa Order)
            modelBuilder.Entity<OrderItem>()
                .HasRequired(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .WillCascadeOnDelete(true);

            // OrderItem -> Product (tắt cascade để không xóa item khi xóa product)
            modelBuilder.Entity<OrderItem>()
                .HasRequired(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .WillCascadeOnDelete(false);

            // ProductImage -> Product (tuỳ bạn muốn xóa ảnh khi xóa sản phẩm)
            modelBuilder.Entity<ProductImage>()
                .HasRequired(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId)
                .WillCascadeOnDelete(true);

            // Product ↔ Gemstone (1-1)
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Gemstone)
                .WithMany()
                .HasForeignKey(p => p.GemstoneId)
                .WillCascadeOnDelete(false);

            // Product → Certificate (1-1, Certificate thuộc Gemstone)
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Certificate)
                .WithMany()
                .HasForeignKey(p => p.CertificateId)
                .WillCascadeOnDelete(false);

            // Certificate → Gemstone (1-nhiều)
            modelBuilder.Entity<Certificate>()
                .HasRequired(c => c.Gemstone)
                .WithMany(g => g.Certificates)
                .HasForeignKey(c => c.GemstoneId)
                .WillCascadeOnDelete(false);
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}