using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using WarehouseWebMVC.Models.Domain;

namespace WarehouseWebMVC.Data
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Name = "Trần Đức Huy",
                    Email = "huytdqe170235@fpt.edu.vn",
                    Address = "Quy Nhon",
                    Phone = "0123456789",
                    Password = "123456",
                    Role = "BE"
                },
                new User
                {
                    UserId = 2,
                    Name = "Nguyễn Xuân Quý",
                    Email = "quynxqe170239@fpt.edu.vn",
                    Address = "Quy Nhon",
                    Phone = "0123456788",
                    Password = "123456",
                    Role = "FE"
                },
                new User
                {
                    UserId = 3,
                    Name = "Trần Ngọc Sang",
                    Email = "sangtnqe170193@fpt.edu.vn",
                    Address = "Quy Nhon",
                    Phone = "0123456787",
                    Password = "123456",
                    Role = "FE"
                },
                new User
                {
                    UserId = 4,
                    Name = "Ngô Gia Hoàng",
                    Email = "hoangngqe170225@fpt.edu.vn",
                    Address = "Quy Nhon",
                    Phone = "0123456786",
                    Password = "123456",
                    Role = "BE"
                },
                new User
                {
                    UserId = 5,
                    Name = "Nguyễn Nhật Hào",
                    Email = "haonnqe170204@fpt.edu.vn",
                    Address = "Quy Nhon",
                    Phone = "0123456785",
                    Password = "123456",
                    Role = "FE"
                },
                new User
                {
                    UserId = 6,
                    Name = "Nguyễn Đào Minh Thuận",
                    Email = "thuanndmqe170240@fpt.edu.vn",
                    Address = "Quy Nhon",
                    Phone = "0123456784",
                    Password = "123456",
                    Role = "BE"
                }
            );

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { SupplierId = 1, Name = "Supplier A", Email = "supplierA@gmail.com", 
                    Address = "Supplier A Address", Phone = "0987654321", Fax = "123456" },    
                new Supplier { SupplierId = 2, Name = "Supplier B", Email = "supplierB@gmail.com", 
                    Address = "Supplier B Address", Phone = "0987654322", Fax = "123457" },    
                new Supplier { SupplierId = 3, Name = "Supplier C", Email = "supplierC@gmail.com", 
                    Address = "Supplier C Address", Phone = "0987654323", Fax = "123458" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Laptop" },    
                new Category { CategoryId = 2, Name = "Phone" },    
                new Category { CategoryId = 3, Name = "Shoes" }    
            );

            modelBuilder.Entity<Brand>().HasData(
                new Brand { BrandId = 1, Name = "Apple" },
                new Brand { BrandId = 2, Name = "Samsung" },
                new Brand { BrandId = 3, Name = "Nike" },
                new Brand { BrandId = 4, Name = "Adidas" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { 
                    ProductId = 1, Name = "Apple 2023 MacBook Pro Laptop M3 Pro", 
                    Description = "18GB Unified Memory, 512GB SSD Storage. Works with iPhone/iPad; Space Black",
                    SupplierId = 1, Price = 1399.99, CategoryId = 1, BrandId = 1, Unit = "Piece"
                },
                new Product { 
                    ProductId = 2, Name = "Apple iPhone 15 Pro Max (512 GB)", 
                    Description = "iPhone 15 Pro Max has a strong and light aerospace-grade titanium design with a textured matte-glass back.&nbsp;",
                    SupplierId = 1, Price = 1199.99, CategoryId = 2, BrandId = 1, Unit = "Piece"
                },
                new Product { 
                    ProductId = 3, Name = "Air Force 1", 
                    Description = "Designed by Bruce Kilgore and introduced in 1982, the Air Force 1 was the first-ever basketball shoe to feature Nike Air technology",
                    SupplierId = 2, Price = 115.00, CategoryId = 3, BrandId = 3, Unit = "Pair"
                },
                new Product { 
                    ProductId = 4, Name = "NMD_R1 SHOES", 
                    Description = "With these adidas NMD_R1 shoes, all it takes is seconds. Seconds, and you're comfortable, ready to go, out the door.",
                    SupplierId = 3, Price = 150, CategoryId = 3, BrandId = 4, Unit = "Pair"
                },
                new Product { 
                    ProductId = 5, Name = "FORUM LOW SHOES", 
                    Description = "More than just a shoe, it's a statement. The adidas Forum hit the scene in '84 and gained major love on both the hardwood and in the music biz.",
                    SupplierId = 3, Price = 99.99, CategoryId = 3, BrandId = 4, Unit = "Pair"
                }
            );

            modelBuilder.Entity<ProductImg>().HasData(
                new ProductImg { ImageId = 1, ProductId = 1, 
                    ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/productImage%2FLaptop%2FMacM3_.jpg?alt=media&token=f6804fd2-0fc0-4f49-a511-ba11ebb1c995" },
                new ProductImg { ImageId = 2, ProductId = 1, 
                    ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/productImage%2FLaptop%2FMacM3_2.jpg?alt=media&token=f63dfb8f-9473-4921-846c-e378a8ad25c9" },
                new ProductImg { ImageId = 3, ProductId = 2, 
                    ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/productImage%2FPhone%2F15prom_.jpg?alt=media&token=836e868b-0965-4566-83c1-78e92e9d8099" },
                new ProductImg { ImageId = 4, ProductId = 2, 
                    ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/productImage%2FPhone%2F15prom_2.jpg?alt=media&token=a7b044d8-9298-44df-9157-7829d6834a8b" },
                new ProductImg { ImageId = 5, ProductId = 3, 
                    ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2Fair-force-1-07-easyon-shoes-lpjTWM.png?alt=media&token=5247ee73-0aff-4ad7-bd2b-a3f5bbedde8f" },
                new ProductImg { ImageId = 6, ProductId = 3, 
                    ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2Fair-force-1-07-easyon-shoes-lpjTWM%20(1).png?alt=media&token=7c4acd0b-2dbe-47f4-9c73-1ef04a74b459" },
                new ProductImg { ImageId = 7, ProductId = 4, 
                    ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2FNMD_R1_Shoes_White_HQ4451_02_standard_hover.avif?alt=media&token=48f6f0f0-6ea9-491e-a7e2-4cd4a2467a7d" },
                new ProductImg { ImageId = 8, ProductId = 4, 
                    ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2FNMD_R1_Shoes_White_HQ4451_01_standard.avif?alt=media&token=16bdfe85-910a-4ae5-b120-27f28e06dc71" },
                new ProductImg { ImageId = 9, ProductId = 5, 
                    ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2FForum_Low_Shoes_White_FY7755_01_standard.avif?alt=media&token=e9cf282a-e6bc-445f-89e7-f270b768f500" },
                new ProductImg { ImageId = 10, ProductId = 5, 
                    ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2FForum_Low_Shoes_White_FY7755_02_standard_hover.avif?alt=media&token=06650fd5-295c-45b3-b698-8b320d76a7f0" }
            );

            modelBuilder.Entity<Invoice>().HasData(
                new Invoice { InvoiceId = 1, Total = 17599.87, UserId = 1, SupplierId = 1 },
                new Invoice { InvoiceId = 2, Total = 2300, UserId = 1, SupplierId = 2 },
                new Invoice { InvoiceId = 3, Total = 1999.84, UserId = 2, SupplierId = 3 }
            );

            modelBuilder.Entity<InvoiceDetail>().HasData(
                new InvoiceDetail { InvoiceDetailId = 1, InvoiceId = 1, ProductId = 1, ImportPrice = 1399.99, Quantity = 10 },
                new InvoiceDetail { InvoiceDetailId = 2, InvoiceId = 1, ProductId = 2, ImportPrice = 1199.99, Quantity = 3 },
                new InvoiceDetail { InvoiceDetailId = 3, InvoiceId = 2, ProductId = 3, ImportPrice = 115, Quantity = 20 },
                new InvoiceDetail { InvoiceDetailId = 4, InvoiceId = 3, ProductId = 5, ImportPrice = 99.99, Quantity = 16 }
            );

            modelBuilder.Entity<ExpenseReport>().HasData(
                new ExpenseReport { ExpenseReportId = 1, Reason = "Enter new Laptops and Phones into warehouse", Total = 17599.87, SenderId = 1, ReceiverId = 2 },
                new ExpenseReport { ExpenseReportId = 2, Reason = "Enter new Shoes into warehouse", Total = 2300, SenderId = 1, ReceiverId = 3, },
                new ExpenseReport { ExpenseReportId = 3, Reason = "Enter new Shoes into warehouse", Total = 1999.84, SenderId = 2, ReceiverId = 4 }
            );

            modelBuilder.Entity<Warehouse>().HasData(
                new Warehouse { WarehouseId = 1, ProductId = 1, Quantity = 10, QuantityAtBeginPeriod = 0, QuantityImport = 10, PriceImport = 1399.99 },
                new Warehouse { WarehouseId = 2, ProductId = 2, Quantity = 3, QuantityAtBeginPeriod = 0, QuantityImport = 3, PriceImport = 1199.99 },
                new Warehouse { WarehouseId = 3, ProductId = 3, Quantity = 20, QuantityAtBeginPeriod = 0, QuantityImport = 20, PriceImport = 115 },
                new Warehouse { WarehouseId = 4, ProductId = 4, Quantity = 16, QuantityAtBeginPeriod = 0, QuantityImport = 16, PriceImport = 99.99 }
            );
        }
    }
}
