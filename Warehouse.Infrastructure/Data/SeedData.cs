﻿using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Data;

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
                Address = "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định",
                Phone = "0963456789",
                Password = "$2y$10$40DFL/Py8ND8Bdfir4EdTODhzj.JEy3WESjKc6GKYqYkkFx86UtEG",
                Avatar = "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky5.jpg?alt=media&token=89ff6391-2c89-4e62-a40e-1f96c5414071",
                Role = "BE"
            },
            new User
            {
                UserId = 2,
                Name = "Nguyễn Xuân Quý",
                Email = "quynxqe170239@fpt.edu.vn",
                Address = "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định",
                Phone = "0963456788",
                Password = "$2y$10$hTD60Pf9h9e6bqhRIOMGnuLKDO0Wd7ZpjnEEkbMpwibIYnRClJz.K",
                Avatar = "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky1.jpg?alt=media&token=20f7f936-db7d-4498-9245-50875cc9f546",
                Role = "FE"
            },
            new User
            {
                UserId = 3,
                Name = "Trần Ngọc Sang",
                Email = "sangtnqe170193@fpt.edu.vn",
                Address = "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định",
                Phone = "0963456787",
                Password = "$2y$10$TMJf55QpiswSvGhC63SFmuUsmogxdQx8k2dwL2QTvXCyurlJpgxZO",
                Avatar = "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky2.jpg?alt=media&token=67c90174-f0e6-4251-acdb-e17d9d88e8ec",
                Role = "FE"
            },
            new User
            {
                UserId = 4,
                Name = "Ngô Gia Hoàng",
                Email = "hoangngqe170225@fpt.edu.vn",
                Address = "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định",
                Phone = "0963456786",
                Password = "$2y$10$XKo8.bgvr/ZzPcET1sZb4.NvTb97LPfC5uNG04hZBLzXT.5qkJI.G",
                Avatar = "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky3.jpg?alt=media&token=2b2622f9-9b99-4ab4-bbc9-aa3c66dd7b24",
                Role = "BE"
            },
            new User
            {
                UserId = 5,
                Name = "Nguyễn Nhật Hào",
                Email = "haonnqe170204@fpt.edu.vn",
                Address = "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định",
                Phone = "0963456785",
                Password = "$2y$10$O0/b51uLUnO7W7ewUB1UoOnevFdVLLxQjU3R7PD7fD3LRZcvEAXlC",
                Avatar = "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky4.jpg?alt=media&token=cbd4f161-7102-4ce1-b9f2-1ccf0c9edf57",
                Role = "FE"
            },
            new User
            {
                UserId = 6,
                Name = "Nguyễn Đào Minh Thuận",
                Email = "thuanndmqe170240@fpt.edu.vn",
                Address = "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định",
                Phone = "0963456784",
                Password = "$2y$10$yUCV0ag395.rVlDjMTOzzuh9psKpf94DajeJVYgWjuEnIc/4ftpx.",
                Avatar = "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky6.webp?alt=media&token=85db917a-6c50-4860-948d-266409314974",
                Role = "BE"
            },
            new User
            {
                UserId = 7,
                Name = "Nguyễn Director",
                Email = "nguyendirector@gmail.com",
                Address = "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định",
                Phone = "0963456712",
                Password = "$2y$10$yUCV0ag395.rVlDjMTOzzuh9psKpf94DajeJVYgWjuEnIc/4ftpx.",
                Avatar = "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky6.webp?alt=media&token=85db917a-6c50-4860-948d-266409314974",
                Role = "Director"
            },
            new User
            {
                UserId = 8,
                Name = "Ngô Staff",
                Email = "ngostaff@gmail.com",
                Address = "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định",
                Phone = "0963456711",
                Password = "$2y$10$yUCV0ag395.rVlDjMTOzzuh9psKpf94DajeJVYgWjuEnIc/4ftpx.",
                Avatar = "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky6.webp?alt=media&token=85db917a-6c50-4860-948d-266409314974",
                Role = "Staff"
            }
        );

        modelBuilder.Entity<Supplier>().HasData(
            new Supplier
            {
                SupplierId = 1,
                Name = "Supplier A",
                Email = "supplierA@gmail.com",
                Address = "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định",
                Phone = "0987654321",
                Fax = "123456"
            },
            new Supplier
            {
                SupplierId = 2,
                Name = "Supplier B",
                Email = "supplierB@gmail.com",
                Address = "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định",
                Phone = "0987654322",
                Fax = "123457"
            },
            new Supplier
            {
                SupplierId = 3,
                Name = "Supplier C",
                Email = "supplierC@gmail.com",
                Address = "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định",
                Phone = "0987654323",
                Fax = "123458"
            }
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
            new Product
            {
                ProductId = 1,
                Name = "Apple 2023 MacBook Pro Laptop M3 Pro",
                Description = "18GB Unified Memory, 512GB SSD Storage. Works with iPhone/iPad; Space Black",
                Price = 1399.99,
                CategoryId = 1,
                BrandId = 1,
                Unit = "Piece"
            },
            new Product
            {
                ProductId = 2,
                Name = "Apple iPhone 15 Pro Max (512 GB)",
                Description = "iPhone 15 Pro Max has a strong and light aerospace-grade titanium design with a textured matte-glass back.&nbsp;",
                Price = 1199.99,
                CategoryId = 2,
                BrandId = 1,
                Unit = "Piece"
            },
            new Product
            {
                ProductId = 3,
                Name = "Air Force 1",
                Description = "Designed by Bruce Kilgore and introduced in 1982, the Air Force 1 was the first-ever basketball shoe to feature Nike Air technology",
                Price = 115.00,
                CategoryId = 3,
                BrandId = 3,
                Unit = "Pair"
            },
            new Product
            {
                ProductId = 4,
                Name = "NMD_R1 SHOES",
                Description = "With these adidas NMD_R1 shoes, all it takes is seconds. Seconds, and you're comfortable, ready to go, out the door.",
                Price = 150,
                CategoryId = 3,
                BrandId = 4,
                Unit = "Pair"
            },
            new Product
            {
                ProductId = 5,
                Name = "FORUM LOW SHOES",
                Description = "More than just a shoe, it's a statement. The adidas Forum hit the scene in '84 and gained major love on both the hardwood and in the music biz.",
                Price = 99.99,
                CategoryId = 3,
                BrandId = 4,
                Unit = "Pair"
            }
        );

        modelBuilder.Entity<ProductImg>().HasData(
            new ProductImg
            {
                ImageId = 1,
                ProductId = 1,
                ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/productImage%2FLaptop%2FMacM3_.jpg?alt=media&token=f6804fd2-0fc0-4f49-a511-ba11ebb1c995"
            },
            new ProductImg
            {
                ImageId = 2,
                ProductId = 1,
                ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/productImage%2FLaptop%2FMacM3_2.jpg?alt=media&token=f63dfb8f-9473-4921-846c-e378a8ad25c9"
            },
            new ProductImg
            {
                ImageId = 3,
                ProductId = 2,
                ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/productImage%2FPhone%2F15prom_.jpg?alt=media&token=836e868b-0965-4566-83c1-78e92e9d8099"
            },
            new ProductImg
            {
                ImageId = 4,
                ProductId = 2,
                ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/productImage%2FPhone%2F15prom_2.jpg?alt=media&token=a7b044d8-9298-44df-9157-7829d6834a8b"
            },
            new ProductImg
            {
                ImageId = 5,
                ProductId = 3,
                ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2Fair-force-1-07-easyon-shoes-lpjTWM.png?alt=media&token=5247ee73-0aff-4ad7-bd2b-a3f5bbedde8f"
            },
            new ProductImg
            {
                ImageId = 6,
                ProductId = 3,
                ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2Fair-force-1-07-easyon-shoes-lpjTWM%20(1).png?alt=media&token=7c4acd0b-2dbe-47f4-9c73-1ef04a74b459"
            },
            new ProductImg
            {
                ImageId = 7,
                ProductId = 4,
                ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2FNMD_R1_Shoes_White_HQ4451_02_standard_hover.avif?alt=media&token=48f6f0f0-6ea9-491e-a7e2-4cd4a2467a7d"
            },
            new ProductImg
            {
                ImageId = 8,
                ProductId = 4,
                ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2FNMD_R1_Shoes_White_HQ4451_01_standard.avif?alt=media&token=16bdfe85-910a-4ae5-b120-27f28e06dc71"
            },
            new ProductImg
            {
                ImageId = 9,
                ProductId = 5,
                ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2FForum_Low_Shoes_White_FY7755_01_standard.avif?alt=media&token=e9cf282a-e6bc-445f-89e7-f270b768f500"
            },
            new ProductImg
            {
                ImageId = 10,
                ProductId = 5,
                ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2FForum_Low_Shoes_White_FY7755_02_standard_hover.avif?alt=media&token=06650fd5-295c-45b3-b698-8b320d76a7f0"
            }
        );

        modelBuilder.Entity<ImportNote>().HasData(
            new ImportNote
            {
                ImportNoteId = 1,
                Deliverer = "Nguyễn Xuân B",
                Reason = "Import Product",
                ReasonDetail = "Reason Detail...",
                Total = 17599.87,
                UserId = 1,
                SupplierId = 1
            },
            new ImportNote
            {
                ImportNoteId = 2,
                Deliverer = "Nguyễn Xuân B",
                Reason = "Tranferred Warehouse",
                ReasonDetail = "Reason Detail...",
                Total = 2300,
                UserId = 1,
                SupplierId = 2
            },
            new ImportNote
            {
                ImportNoteId = 3,
                Deliverer = "Trần Đức A",
                Reason = "Import Product",
                ReasonDetail = "Reason Detail...",
                Total = 3399.3,
                UserId = 2,
                SupplierId = 3
            }
        );

        modelBuilder.Entity<ImportNoteDetail>().HasData(
            new ImportNoteDetail { ImportNoteDetailId = 1, ImportNoteId = 1, ProductId = 1, ImportPrice = 1399.99, Quantity = 10 },
            new ImportNoteDetail { ImportNoteDetailId = 2, ImportNoteId = 1, ProductId = 2, ImportPrice = 1199.99, Quantity = 3 },
            new ImportNoteDetail { ImportNoteDetailId = 3, ImportNoteId = 2, ProductId = 3, ImportPrice = 115, Quantity = 20 },
            new ImportNoteDetail { ImportNoteDetailId = 4, ImportNoteId = 3, ProductId = 4, ImportPrice = 150, Quantity = 16 },
            new ImportNoteDetail { ImportNoteDetailId = 5, ImportNoteId = 3, ProductId = 5, ImportPrice = 99.99, Quantity = 10 }
        );

        modelBuilder.Entity<ExpenseReport>().HasData(
            new ExpenseReport { ExpenseReportId = 1, Reason = "Enter new Laptops and Phones into warehouse", Total = 17599.87, SenderId = 1, ReceiverId = 2 },
            new ExpenseReport { ExpenseReportId = 2, Reason = "Enter new Shoes into warehouse", Total = 2300, SenderId = 1, ReceiverId = 3, },
            new ExpenseReport { ExpenseReportId = 3, Reason = "Enter new Shoes into warehouse", Total = 3399.9, SenderId = 2, ReceiverId = 4 }
        );

        modelBuilder.Entity<WarehouseE>().HasData(
            new WarehouseE { WarehouseId = 1, ProductId = 1, Quantity = 10, QuantityAtBeginPeriod = 0, QuantityImport = 10, PriceImport = 1399.99 },
            new WarehouseE { WarehouseId = 2, ProductId = 2, Quantity = 3, QuantityAtBeginPeriod = 0, QuantityImport = 3, PriceImport = 1199.99 },
            new WarehouseE { WarehouseId = 3, ProductId = 3, Quantity = 20, QuantityAtBeginPeriod = 0, QuantityImport = 20, PriceImport = 115 },
            new WarehouseE { WarehouseId = 4, ProductId = 4, Quantity = 16, QuantityAtBeginPeriod = 0, QuantityImport = 16, PriceImport = 150 },
            new WarehouseE { WarehouseId = 5, ProductId = 5, Quantity = 0, QuantityAtBeginPeriod = 0, QuantityImport = 10, PriceImport = 99.99 }
        );
    }
}
