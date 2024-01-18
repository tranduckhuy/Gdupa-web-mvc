﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WarehouseWebMVC.Data;

#nullable disable

namespace WarehouseWebMVC.Migrations;

[DbContext(typeof(DataContext))]
partial class DataContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.Brand", b =>
            {
                b.Property<long>("BrandId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.HasKey("BrandId");

                b.ToTable("Brand");

                b.HasData(
                    new
                    {
                        BrandId = 1L,
                        Name = "Apple"
                    },
                    new
                    {
                        BrandId = 2L,
                        Name = "Samsung"
                    },
                    new
                    {
                        BrandId = 3L,
                        Name = "Nike"
                    },
                    new
                    {
                        BrandId = 4L,
                        Name = "Adidas"
                    });
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.Category", b =>
            {
                b.Property<long>("CategoryId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.HasKey("CategoryId");

                b.ToTable("Category");

                b.HasData(
                    new
                    {
                        CategoryId = 1L,
                        Name = "Laptop"
                    },
                    new
                    {
                        CategoryId = 2L,
                        Name = "Phone"
                    },
                    new
                    {
                        CategoryId = 3L,
                        Name = "Shoes"
                    });
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.ExpenseReport", b =>
            {
                b.Property<long>("ExpenseReportId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<DateTime>("CreatedAt")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("TEXT")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<string>("Reason")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<long>("ReceiverId")
                    .HasColumnType("INTEGER");

                b.Property<long>("SenderId")
                    .HasColumnType("INTEGER");

                b.Property<double>("Total")
                    .HasColumnType("REAL");

                b.HasKey("ExpenseReportId");

                b.HasIndex("ReceiverId");

                b.HasIndex("SenderId");

                b.ToTable("Expenses");

                b.HasData(
                    new
                    {
                        ExpenseReportId = 1L,
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Reason = "Enter new Laptops and Phones into warehouse",
                        ReceiverId = 2L,
                        SenderId = 1L,
                        Total = 17599.869999999999
                    },
                    new
                    {
                        ExpenseReportId = 2L,
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Reason = "Enter new Shoes into warehouse",
                        ReceiverId = 3L,
                        SenderId = 1L,
                        Total = 2300.0
                    },
                    new
                    {
                        ExpenseReportId = 3L,
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Reason = "Enter new Shoes into warehouse",
                        ReceiverId = 4L,
                        SenderId = 2L,
                        Total = 1999.8399999999999
                    });
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.Invoice", b =>
            {
                b.Property<long>("InvoiceId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<DateTime>("CreatedAt")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("TEXT")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<long>("SupplierId")
                    .HasColumnType("INTEGER");

                b.Property<double>("Total")
                    .HasColumnType("REAL");

                b.Property<long>("UserId")
                    .HasColumnType("INTEGER");

                b.HasKey("InvoiceId");

                b.HasIndex("SupplierId");

                b.HasIndex("UserId");

                b.ToTable("Invoices");

                b.HasData(
                    new
                    {
                        InvoiceId = 1L,
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        SupplierId = 1L,
                        Total = 17599.869999999999,
                        UserId = 1L
                    },
                    new
                    {
                        InvoiceId = 2L,
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        SupplierId = 2L,
                        Total = 2300.0,
                        UserId = 1L
                    },
                    new
                    {
                        InvoiceId = 3L,
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        SupplierId = 3L,
                        Total = 1999.8399999999999,
                        UserId = 2L
                    });
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.InvoiceDetail", b =>
            {
                b.Property<long>("InvoiceDetailId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<double>("ImportPrice")
                    .HasColumnType("REAL");

                b.Property<long>("InvoiceId")
                    .HasColumnType("INTEGER");

                b.Property<long>("ProductId")
                    .HasColumnType("INTEGER");

                b.Property<int>("Quantity")
                    .HasColumnType("INTEGER");

                b.HasKey("InvoiceDetailId");

                b.HasIndex("InvoiceId");

                b.HasIndex("ProductId");

                b.ToTable("InvoicesDetails");

                b.HasData(
                    new
                    {
                        InvoiceDetailId = 1L,
                        ImportPrice = 1399.99,
                        InvoiceId = 1L,
                        ProductId = 1L,
                        Quantity = 10
                    },
                    new
                    {
                        InvoiceDetailId = 2L,
                        ImportPrice = 1199.99,
                        InvoiceId = 1L,
                        ProductId = 2L,
                        Quantity = 3
                    },
                    new
                    {
                        InvoiceDetailId = 3L,
                        ImportPrice = 115.0,
                        InvoiceId = 2L,
                        ProductId = 3L,
                        Quantity = 20
                    },
                    new
                    {
                        InvoiceDetailId = 4L,
                        ImportPrice = 99.989999999999995,
                        InvoiceId = 3L,
                        ProductId = 5L,
                        Quantity = 16
                    });
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.Product", b =>
            {
                b.Property<long>("ProductId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<long>("BrandId")
                    .HasColumnType("INTEGER");

                b.Property<long>("CategoryId")
                    .HasColumnType("INTEGER");

                b.Property<DateTime>("CreatedAt")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("TEXT")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<DateTime?>("ModifiedAt")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("TEXT")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("TEXT");

                b.Property<double>("Price")
                    .HasColumnType("REAL");

                b.Property<long>("SupplierId")
                    .HasColumnType("INTEGER");

                b.Property<string>("Unit")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("TEXT");

                b.HasKey("ProductId");

                b.HasIndex("BrandId");

                b.HasIndex("CategoryId");

                b.HasIndex("SupplierId");

                b.ToTable("Products");

                b.HasData(
                    new
                    {
                        ProductId = 1L,
                        BrandId = 1L,
                        CategoryId = 1L,
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Description = "18GB Unified Memory, 512GB SSD Storage. Works with iPhone/iPad; Space Black",
                        Name = "Apple 2023 MacBook Pro Laptop M3 Pro",
                        Price = 1399.99,
                        SupplierId = 1L,
                        Unit = "Piece"
                    },
                    new
                    {
                        ProductId = 2L,
                        BrandId = 1L,
                        CategoryId = 2L,
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Description = "iPhone 15 Pro Max has a strong and light aerospace-grade titanium design with a textured matte-glass back.&nbsp;",
                        Name = "Apple iPhone 15 Pro Max (512 GB)",
                        Price = 1199.99,
                        SupplierId = 1L,
                        Unit = "Piece"
                    },
                    new
                    {
                        ProductId = 3L,
                        BrandId = 3L,
                        CategoryId = 3L,
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Description = "Designed by Bruce Kilgore and introduced in 1982, the Air Force 1 was the first-ever basketball shoe to feature Nike Air technology",
                        Name = "Air Force 1",
                        Price = 115.0,
                        SupplierId = 2L,
                        Unit = "Pair"
                    },
                    new
                    {
                        ProductId = 4L,
                        BrandId = 4L,
                        CategoryId = 3L,
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Description = "With these adidas NMD_R1 shoes, all it takes is seconds. Seconds, and you're comfortable, ready to go, out the door.",
                        Name = "NMD_R1 SHOES",
                        Price = 150.0,
                        SupplierId = 3L,
                        Unit = "Pair"
                    },
                    new
                    {
                        ProductId = 5L,
                        BrandId = 4L,
                        CategoryId = 3L,
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Description = "More than just a shoe, it's a statement. The adidas Forum hit the scene in '84 and gained major love on both the hardwood and in the music biz.",
                        Name = "FORUM LOW SHOES",
                        Price = 99.989999999999995,
                        SupplierId = 3L,
                        Unit = "Pair"
                    });
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.ProductImg", b =>
            {
                b.Property<long>("ImageId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<string>("ImageURL")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<long>("ProductId")
                    .HasColumnType("INTEGER");

                b.HasKey("ImageId");

                b.HasIndex("ProductId");

                b.ToTable("ProductImgs");

                b.HasData(
                    new
                    {
                        ImageId = 1L,
                        ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/productImage%2FLaptop%2FMacM3_.jpg?alt=media&token=f6804fd2-0fc0-4f49-a511-ba11ebb1c995",
                        ProductId = 1L
                    },
                    new
                    {
                        ImageId = 2L,
                        ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/productImage%2FLaptop%2FMacM3_2.jpg?alt=media&token=f63dfb8f-9473-4921-846c-e378a8ad25c9",
                        ProductId = 1L
                    },
                    new
                    {
                        ImageId = 3L,
                        ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/productImage%2FPhone%2F15prom_.jpg?alt=media&token=836e868b-0965-4566-83c1-78e92e9d8099",
                        ProductId = 2L
                    },
                    new
                    {
                        ImageId = 4L,
                        ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/productImage%2FPhone%2F15prom_2.jpg?alt=media&token=a7b044d8-9298-44df-9157-7829d6834a8b",
                        ProductId = 2L
                    },
                    new
                    {
                        ImageId = 5L,
                        ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2Fair-force-1-07-easyon-shoes-lpjTWM.png?alt=media&token=5247ee73-0aff-4ad7-bd2b-a3f5bbedde8f",
                        ProductId = 3L
                    },
                    new
                    {
                        ImageId = 6L,
                        ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2Fair-force-1-07-easyon-shoes-lpjTWM%20(1).png?alt=media&token=7c4acd0b-2dbe-47f4-9c73-1ef04a74b459",
                        ProductId = 3L
                    },
                    new
                    {
                        ImageId = 7L,
                        ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2FNMD_R1_Shoes_White_HQ4451_02_standard_hover.avif?alt=media&token=48f6f0f0-6ea9-491e-a7e2-4cd4a2467a7d",
                        ProductId = 4L
                    },
                    new
                    {
                        ImageId = 8L,
                        ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2FNMD_R1_Shoes_White_HQ4451_01_standard.avif?alt=media&token=16bdfe85-910a-4ae5-b120-27f28e06dc71",
                        ProductId = 4L
                    },
                    new
                    {
                        ImageId = 9L,
                        ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2FForum_Low_Shoes_White_FY7755_01_standard.avif?alt=media&token=e9cf282a-e6bc-445f-89e7-f270b768f500",
                        ProductId = 5L
                    },
                    new
                    {
                        ImageId = 10L,
                        ImageURL = "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2FForum_Low_Shoes_White_FY7755_02_standard_hover.avif?alt=media&token=06650fd5-295c-45b3-b698-8b320d76a7f0",
                        ProductId = 5L
                    });
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.Supplier", b =>
            {
                b.Property<long>("SupplierId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<string>("Address")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("TEXT");

                b.Property<string>("Fax")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("TEXT");

                b.Property<string>("Phone")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("TEXT");

                b.HasKey("SupplierId");

                b.ToTable("Suppliers");

                b.HasData(
                    new
                    {
                        SupplierId = 1L,
                        Address = "Supplier A Address",
                        Email = "supplierA@gmail.com",
                        Fax = "123456",
                        Name = "Supplier A",
                        Phone = "0987654321"
                    },
                    new
                    {
                        SupplierId = 2L,
                        Address = "Supplier B Address",
                        Email = "supplierB@gmail.com",
                        Fax = "123457",
                        Name = "Supplier B",
                        Phone = "0987654322"
                    },
                    new
                    {
                        SupplierId = 3L,
                        Address = "Supplier C Address",
                        Email = "supplierC@gmail.com",
                        Fax = "123458",
                        Name = "Supplier C",
                        Phone = "0987654323"
                    });
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.User", b =>
            {
                b.Property<long>("UserId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<string>("Address")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<DateTime>("CreatedAt")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("TEXT")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("TEXT");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("TEXT");

                b.Property<string>("Password")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("TEXT");

                b.Property<string>("Phone")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("TEXT");

                b.Property<string>("Role")
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnType("TEXT");

                b.HasKey("UserId");

                b.ToTable("Users");

                b.HasData(
                    new
                    {
                        UserId = 1L,
                        Address = "Quy Nhon",
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Email = "huytdqe170235@fpt.edu.vn",
                        Name = "Trần Đức Huy",
                        Password = "123456",
                        Phone = "0123456789",
                        Role = "BE"
                    },
                    new
                    {
                        UserId = 2L,
                        Address = "Quy Nhon",
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Email = "quynxqe170239@fpt.edu.vn",
                        Name = "Nguyễn Xuân Quý",
                        Password = "123456",
                        Phone = "0123456788",
                        Role = "FE"
                    },
                    new
                    {
                        UserId = 3L,
                        Address = "Quy Nhon",
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Email = "sangtnqe170193@fpt.edu.vn",
                        Name = "Trần Ngọc Sang",
                        Password = "123456",
                        Phone = "0123456787",
                        Role = "FE"
                    },
                    new
                    {
                        UserId = 4L,
                        Address = "Quy Nhon",
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Email = "hoangngqe170225@fpt.edu.vn",
                        Name = "Ngô Gia Hoàng",
                        Password = "123456",
                        Phone = "0123456786",
                        Role = "BE"
                    },
                    new
                    {
                        UserId = 5L,
                        Address = "Quy Nhon",
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Email = "haonnqe170204@fpt.edu.vn",
                        Name = "Nguyễn Nhật Hào",
                        Password = "123456",
                        Phone = "0123456785",
                        Role = "FE"
                    },
                    new
                    {
                        UserId = 6L,
                        Address = "Quy Nhon",
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Email = "thuanndmqe170240@fpt.edu.vn",
                        Name = "Nguyễn Đào Minh Thuận",
                        Password = "123456",
                        Phone = "0123456784",
                        Role = "BE"
                    });
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.Warehouse", b =>
            {
                b.Property<long>("WarehouseId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<DateTime>("CreatedAt")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("TEXT")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<double>("PriceImport")
                    .HasColumnType("REAL");

                b.Property<long>("ProductId")
                    .HasColumnType("INTEGER");

                b.Property<int>("Quantity")
                    .HasColumnType("INTEGER");

                b.Property<int>("QuantityAtBeginPeriod")
                    .HasColumnType("INTEGER");

                b.Property<int>("QuantityImport")
                    .HasColumnType("INTEGER");

                b.HasKey("WarehouseId");

                b.HasIndex("ProductId");

                b.ToTable("Warehouse");

                b.HasData(
                    new
                    {
                        WarehouseId = 1L,
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        PriceImport = 1399.99,
                        ProductId = 1L,
                        Quantity = 10,
                        QuantityAtBeginPeriod = 0,
                        QuantityImport = 10
                    },
                    new
                    {
                        WarehouseId = 2L,
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        PriceImport = 1199.99,
                        ProductId = 2L,
                        Quantity = 3,
                        QuantityAtBeginPeriod = 0,
                        QuantityImport = 3
                    },
                    new
                    {
                        WarehouseId = 3L,
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        PriceImport = 115.0,
                        ProductId = 3L,
                        Quantity = 20,
                        QuantityAtBeginPeriod = 0,
                        QuantityImport = 20
                    },
                    new
                    {
                        WarehouseId = 4L,
                        CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        PriceImport = 99.989999999999995,
                        ProductId = 4L,
                        Quantity = 16,
                        QuantityAtBeginPeriod = 0,
                        QuantityImport = 16
                    });
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.ExpenseReport", b =>
            {
                b.HasOne("WarehouseWebMVC.Models.Domain.User", "Receiver")
                    .WithMany("ReceivedExpenseReports")
                    .HasForeignKey("ReceiverId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                b.HasOne("WarehouseWebMVC.Models.Domain.User", "Sender")
                    .WithMany("SentExpenseReports")
                    .HasForeignKey("SenderId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                b.Navigation("Receiver");

                b.Navigation("Sender");
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.Invoice", b =>
            {
                b.HasOne("WarehouseWebMVC.Models.Domain.Supplier", "Supplier")
                    .WithMany("Invoices")
                    .HasForeignKey("SupplierId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("WarehouseWebMVC.Models.Domain.User", "User")
                    .WithMany("Invoices")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Supplier");

                b.Navigation("User");
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.InvoiceDetail", b =>
            {
                b.HasOne("WarehouseWebMVC.Models.Domain.Invoice", "Invoice")
                    .WithMany("InvoiceDetails")
                    .HasForeignKey("InvoiceId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("WarehouseWebMVC.Models.Domain.Product", "Product")
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Invoice");

                b.Navigation("Product");
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.Product", b =>
            {
                b.HasOne("WarehouseWebMVC.Models.Domain.Brand", "Brand")
                    .WithMany("Products")
                    .HasForeignKey("BrandId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("WarehouseWebMVC.Models.Domain.Category", "Category")
                    .WithMany("Products")
                    .HasForeignKey("CategoryId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("WarehouseWebMVC.Models.Domain.Supplier", "Supplier")
                    .WithMany("Products")
                    .HasForeignKey("SupplierId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Brand");

                b.Navigation("Category");

                b.Navigation("Supplier");
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.ProductImg", b =>
            {
                b.HasOne("WarehouseWebMVC.Models.Domain.Product", "Product")
                    .WithMany("ProductImgs")
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Product");
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.Warehouse", b =>
            {
                b.HasOne("WarehouseWebMVC.Models.Domain.Product", "Product")
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Product");
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.Brand", b =>
            {
                b.Navigation("Products");
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.Category", b =>
            {
                b.Navigation("Products");
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.Invoice", b =>
            {
                b.Navigation("InvoiceDetails");
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.Product", b =>
            {
                b.Navigation("ProductImgs");
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.Supplier", b =>
            {
                b.Navigation("Invoices");

                b.Navigation("Products");
            });

        modelBuilder.Entity("WarehouseWebMVC.Models.Domain.User", b =>
            {
                b.Navigation("Invoices");

                b.Navigation("ReceivedExpenseReports");

                b.Navigation("SentExpenseReports");
            });
#pragma warning restore 612, 618
    }
}
