namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(nullable: false),
                        City = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        Phone = c.String(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        PaidAt = c.DateTime(),
                        Status = c.Int(nullable: false),
                        PaymentStatus = c.Int(nullable: false),
                        PaymentMethod = c.String(maxLength: 50),
                        TransactionId = c.String(maxLength: 100),
                        PaymentProvider = c.String(maxLength: 50),
                        ShippingAddressId = c.Int(nullable: false),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GiftMessage = c.String(maxLength: 500),
                        GiftWrap = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.ShippingAddressId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ShippingAddressId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductVariantId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.ProductVariants", t => t.ProductVariantId)
                .Index(t => t.OrderId)
                .Index(t => t.ProductVariantId);
            
            CreateTable(
                "dbo.ProductVariants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        VariantSKU = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockQuantity = c.Int(nullable: false),
                        MetalId = c.Int(nullable: false),
                        Size = c.String(maxLength: 20),
                        Color = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Metals", t => t.MetalId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.MetalId);
            
            CreateTable(
                "dbo.Metals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Purity = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ProductId = c.Int(nullable: false),
                        SelectedSize = c.String(),
                        SelectedColor = c.String(),
                        Quantity = c.Int(nullable: false),
                        AddedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Alias = c.String(maxLength: 300),
                        Description = c.String(),
                        CateId = c.Int(nullable: false),
                        GemstoneId = c.Int(nullable: false),
                        CertificateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CateId, cascadeDelete: true)
                .ForeignKey("dbo.Certificates", t => t.CertificateId)
                .Index(t => t.CateId)
                .Index(t => t.CertificateId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Certificates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        Number = c.String(nullable: false),
                        IssueDate = c.DateTime(nullable: false),
                        FileUrl = c.String(),
                        GemstoneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gemstones", t => t.GemstoneId)
                .Index(t => t.GemstoneId);
            
            CreateTable(
                "dbo.Gemstones",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Cut = c.String(),
                        Clarity = c.String(),
                        Color = c.String(),
                        Carat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Origin = c.String(),
                        Treatment = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false),
                        IsPrimary = c.Boolean(nullable: false),
                        View360Url = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.WishlistItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ProductId = c.Int(nullable: false),
                        AddedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishlistItems", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.WishlistItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CartItems", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductVariants", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Gemstones", "Id", "dbo.Products");
            DropForeignKey("dbo.Products", "CertificateId", "dbo.Certificates");
            DropForeignKey("dbo.Certificates", "GemstoneId", "dbo.Gemstones");
            DropForeignKey("dbo.Products", "CateId", "dbo.Categories");
            DropForeignKey("dbo.Addresses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "ShippingAddressId", "dbo.Addresses");
            DropForeignKey("dbo.OrderItems", "ProductVariantId", "dbo.ProductVariants");
            DropForeignKey("dbo.ProductVariants", "MetalId", "dbo.Metals");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.WishlistItems", new[] { "ProductId" });
            DropIndex("dbo.WishlistItems", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            DropIndex("dbo.Gemstones", new[] { "Id" });
            DropIndex("dbo.Certificates", new[] { "GemstoneId" });
            DropIndex("dbo.Products", new[] { "CertificateId" });
            DropIndex("dbo.Products", new[] { "CateId" });
            DropIndex("dbo.CartItems", new[] { "ProductId" });
            DropIndex("dbo.CartItems", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.ProductVariants", new[] { "MetalId" });
            DropIndex("dbo.ProductVariants", new[] { "ProductId" });
            DropIndex("dbo.OrderItems", new[] { "ProductVariantId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "ShippingAddressId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropTable("dbo.WishlistItems");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProductImages");
            DropTable("dbo.Gemstones");
            DropTable("dbo.Certificates");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.CartItems");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Metals");
            DropTable("dbo.ProductVariants");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Addresses");
        }
    }
}
