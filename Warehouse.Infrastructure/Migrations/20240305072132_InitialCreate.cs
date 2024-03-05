using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Warehouse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    BrandId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Avatar = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Fax = table.Column<string>(type: "TEXT", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    Background = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Avatar = table.Column<string>(type: "TEXT", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CategoryId = table.Column<long>(type: "INTEGER", nullable: false),
                    BrandId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpenseReportId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Reason = table.Column<string>(type: "TEXT", nullable: false),
                    Total = table.Column<double>(type: "REAL", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    SenderId = table.Column<long>(type: "INTEGER", nullable: false),
                    ReceiverId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpenseReportId);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Expenses_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ImportNotes",
                columns: table => new
                {
                    ImportNoteId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Total = table.Column<double>(type: "REAL", nullable: false),
                    Deliverer = table.Column<string>(type: "TEXT", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", nullable: false),
                    ReasonDetail = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    SupplierId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportNotes", x => x.ImportNoteId);
                    table.ForeignKey(
                        name: "FK_ImportNotes_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportNotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImgs",
                columns: table => new
                {
                    ImageId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImageURL = table.Column<string>(type: "TEXT", nullable: false),
                    ProductId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImgs", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_ProductImgs_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    WarehouseId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityAtBeginPeriod = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityImport = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceImport = table.Column<double>(type: "REAL", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ProductId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.WarehouseId);
                    table.ForeignKey(
                        name: "FK_Warehouse_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportNoteDetails",
                columns: table => new
                {
                    ImportNoteDetailId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImportPrice = table.Column<double>(type: "REAL", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    ImportNoteId = table.Column<long>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportNoteDetails", x => x.ImportNoteDetailId);
                    table.ForeignKey(
                        name: "FK_ImportNoteDetails_ImportNotes_ImportNoteId",
                        column: x => x.ImportNoteId,
                        principalTable: "ImportNotes",
                        principalColumn: "ImportNoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportNoteDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brand",
                columns: new[] { "BrandId", "Name" },
                values: new object[,]
                {
                    { 1L, "Apple" },
                    { 2L, "Samsung" },
                    { 3L, "Nike" },
                    { 4L, "Adidas" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1L, "Laptop" },
                    { 2L, "Phone" },
                    { 3L, "Shoes" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "SupplierId", "Address", "Avatar", "Background", "Email", "Fax", "IsLocked", "Name", "Phone" },
                values: new object[,]
                {
                    { 1L, "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định", "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fdefault_avatar.png?alt=media&token=560b08e7-3ab2-453e-aea5-def178730766", "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/supplier-background%2Fprofile-cover.jpg?alt=media&token=cf51dca2-8021-40ee-bd58-66000ab49c10", "supplierA@gmail.com", "123456", false, "Supplier A", "0987654321" },
                    { 2L, "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định", "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fdefault_avatar.png?alt=media&token=560b08e7-3ab2-453e-aea5-def178730766", "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/supplier-background%2Fprofile-cover.jpg?alt=media&token=cf51dca2-8021-40ee-bd58-66000ab49c10", "supplierB@gmail.com", "123457", false, "Supplier B", "0987654322" },
                    { 3L, "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định", "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fdefault_avatar.png?alt=media&token=560b08e7-3ab2-453e-aea5-def178730766", "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/supplier-background%2Fprofile-cover.jpg?alt=media&token=cf51dca2-8021-40ee-bd58-66000ab49c10", "supplierC@gmail.com", "123458", false, "Supplier C", "0987654323" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Avatar", "Email", "IsLocked", "Name", "Password", "Phone", "Role" },
                values: new object[,]
                {
                    { 1L, "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định", "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky5.jpg?alt=media&token=89ff6391-2c89-4e62-a40e-1f96c5414071", "huytdqe170235@fpt.edu.vn", false, "Trần Đức Huy", "$2y$10$40DFL/Py8ND8Bdfir4EdTODhzj.JEy3WESjKc6GKYqYkkFx86UtEG", "0963456789", "BE" },
                    { 2L, "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định", "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky1.jpg?alt=media&token=20f7f936-db7d-4498-9245-50875cc9f546", "quynxqe170239@fpt.edu.vn", false, "Nguyễn Xuân Quý", "$2y$10$hTD60Pf9h9e6bqhRIOMGnuLKDO0Wd7ZpjnEEkbMpwibIYnRClJz.K", "0963456788", "FE" },
                    { 3L, "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định", "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky2.jpg?alt=media&token=67c90174-f0e6-4251-acdb-e17d9d88e8ec", "sangtnqe170193@fpt.edu.vn", false, "Trần Ngọc Sang", "$2y$10$TMJf55QpiswSvGhC63SFmuUsmogxdQx8k2dwL2QTvXCyurlJpgxZO", "0963456787", "FE" },
                    { 4L, "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định", "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky3.jpg?alt=media&token=2b2622f9-9b99-4ab4-bbc9-aa3c66dd7b24", "hoangngqe170225@fpt.edu.vn", false, "Ngô Gia Hoàng", "$2y$10$XKo8.bgvr/ZzPcET1sZb4.NvTb97LPfC5uNG04hZBLzXT.5qkJI.G", "0963456786", "BE" },
                    { 5L, "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định", "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky4.jpg?alt=media&token=cbd4f161-7102-4ce1-b9f2-1ccf0c9edf57", "haonnqe170204@fpt.edu.vn", false, "Nguyễn Nhật Hào", "$2y$10$O0/b51uLUnO7W7ewUB1UoOnevFdVLLxQjU3R7PD7fD3LRZcvEAXlC", "0963456785", "FE" },
                    { 6L, "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định", "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky6.webp?alt=media&token=85db917a-6c50-4860-948d-266409314974", "thuanndmqe170240@fpt.edu.vn", false, "Nguyễn Đào Minh Thuận", "$2y$10$yUCV0ag395.rVlDjMTOzzuh9psKpf94DajeJVYgWjuEnIc/4ftpx.", "0963456784", "BE" },
                    { 7L, "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định", "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky6.webp?alt=media&token=85db917a-6c50-4860-948d-266409314974", "nguyendirector@gmail.com", false, "Nguyễn Director", "$2y$10$yUCV0ag395.rVlDjMTOzzuh9psKpf94DajeJVYgWjuEnIc/4ftpx.", "0963456712", "Director" },
                    { 8L, "08 Tống Phước Phổ, Cao Ốc Long Thịnh, Phường Ghềnh Ráng, Thành Phố Quy Nhơn, Tỉnh Bình Định", "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky6.webp?alt=media&token=85db917a-6c50-4860-948d-266409314974", "ngostaff@gmail.vn", false, "Ngô Staff", "$2y$10$yUCV0ag395.rVlDjMTOzzuh9psKpf94DajeJVYgWjuEnIc/4ftpx.", "0963456711", "Staff" }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "ExpenseReportId", "Reason", "ReceiverId", "SenderId", "Total" },
                values: new object[,]
                {
                    { 1L, "Enter new Laptops and Phones into warehouse", 2L, 1L, 17599.869999999999 },
                    { 2L, "Enter new Shoes into warehouse", 3L, 1L, 2300.0 },
                    { 3L, "Enter new Shoes into warehouse", 4L, 2L, 3399.9000000000001 }
                });

            migrationBuilder.InsertData(
                table: "ImportNotes",
                columns: new[] { "ImportNoteId", "Deliverer", "Reason", "ReasonDetail", "SupplierId", "Total", "UserId" },
                values: new object[,]
                {
                    { 1L, "Nguyễn Xuân B", "Import Product", "Reason Detail...", 1L, 17599.869999999999, 1L },
                    { 2L, "Nguyễn Xuân B", "Tranferred Warehouse", "Reason Detail...", 2L, 2300.0, 1L },
                    { 3L, "Trần Đức A", "Import Product", "Reason Detail...", 3L, 3399.3000000000002, 2L }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "BrandId", "CategoryId", "Description", "Name", "Price", "Unit" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, "18GB Unified Memory, 512GB SSD Storage. Works with iPhone/iPad; Space Black", "Apple 2023 MacBook Pro Laptop M3 Pro", 1399.99, "Piece" },
                    { 2L, 1L, 2L, "iPhone 15 Pro Max has a strong and light aerospace-grade titanium design with a textured matte-glass back.&nbsp;", "Apple iPhone 15 Pro Max (512 GB)", 1199.99, "Piece" },
                    { 3L, 3L, 3L, "Designed by Bruce Kilgore and introduced in 1982, the Air Force 1 was the first-ever basketball shoe to feature Nike Air technology", "Air Force 1", 115.0, "Pair" },
                    { 4L, 4L, 3L, "With these adidas NMD_R1 shoes, all it takes is seconds. Seconds, and you're comfortable, ready to go, out the door.", "NMD_R1 SHOES", 150.0, "Pair" },
                    { 5L, 4L, 3L, "More than just a shoe, it's a statement. The adidas Forum hit the scene in '84 and gained major love on both the hardwood and in the music biz.", "FORUM LOW SHOES", 99.989999999999995, "Pair" }
                });

            migrationBuilder.InsertData(
                table: "ImportNoteDetails",
                columns: new[] { "ImportNoteDetailId", "ImportNoteId", "ImportPrice", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1L, 1L, 1399.99, 1L, 10 },
                    { 2L, 1L, 1199.99, 2L, 3 },
                    { 3L, 2L, 115.0, 3L, 20 },
                    { 4L, 3L, 150.0, 4L, 16 },
                    { 5L, 3L, 99.989999999999995, 5L, 10 }
                });

            migrationBuilder.InsertData(
                table: "ProductImgs",
                columns: new[] { "ImageId", "ImageURL", "ProductId" },
                values: new object[,]
                {
                    { 1L, "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/productImage%2FLaptop%2FMacM3_.jpg?alt=media&token=f6804fd2-0fc0-4f49-a511-ba11ebb1c995", 1L },
                    { 2L, "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/productImage%2FLaptop%2FMacM3_2.jpg?alt=media&token=f63dfb8f-9473-4921-846c-e378a8ad25c9", 1L },
                    { 3L, "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/productImage%2FPhone%2F15prom_.jpg?alt=media&token=836e868b-0965-4566-83c1-78e92e9d8099", 2L },
                    { 4L, "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/productImage%2FPhone%2F15prom_2.jpg?alt=media&token=a7b044d8-9298-44df-9157-7829d6834a8b", 2L },
                    { 5L, "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2Fair-force-1-07-easyon-shoes-lpjTWM.png?alt=media&token=5247ee73-0aff-4ad7-bd2b-a3f5bbedde8f", 3L },
                    { 6L, "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2Fair-force-1-07-easyon-shoes-lpjTWM%20(1).png?alt=media&token=7c4acd0b-2dbe-47f4-9c73-1ef04a74b459", 3L },
                    { 7L, "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2FNMD_R1_Shoes_White_HQ4451_02_standard_hover.avif?alt=media&token=48f6f0f0-6ea9-491e-a7e2-4cd4a2467a7d", 4L },
                    { 8L, "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2FNMD_R1_Shoes_White_HQ4451_01_standard.avif?alt=media&token=16bdfe85-910a-4ae5-b120-27f28e06dc71", 4L },
                    { 9L, "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2FForum_Low_Shoes_White_FY7755_01_standard.avif?alt=media&token=e9cf282a-e6bc-445f-89e7-f270b768f500", 5L },
                    { 10L, "https://firebasestorage.googleapis.com/v0/b/xhobbe-98105.appspot.com/o/logo%2FForum_Low_Shoes_White_FY7755_02_standard_hover.avif?alt=media&token=06650fd5-295c-45b3-b698-8b320d76a7f0", 5L }
                });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "WarehouseId", "PriceImport", "ProductId", "Quantity", "QuantityAtBeginPeriod", "QuantityImport" },
                values: new object[,]
                {
                    { 1L, 1399.99, 1L, 10, 0, 10 },
                    { 2L, 1199.99, 2L, 3, 0, 3 },
                    { 3L, 115.0, 3L, 20, 0, 20 },
                    { 4L, 150.0, 4L, 16, 0, 16 },
                    { 5L, 99.989999999999995, 5L, 0, 0, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ReceiverId",
                table: "Expenses",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_SenderId",
                table: "Expenses",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportNoteDetails_ImportNoteId",
                table: "ImportNoteDetails",
                column: "ImportNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportNoteDetails_ProductId",
                table: "ImportNoteDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportNotes_SupplierId",
                table: "ImportNotes",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportNotes_UserId",
                table: "ImportNotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImgs_ProductId",
                table: "ProductImgs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_ProductId",
                table: "Warehouse",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "ImportNoteDetails");

            migrationBuilder.DropTable(
                name: "ProductImgs");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "ImportNotes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
