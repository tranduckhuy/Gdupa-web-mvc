using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WarehouseWebMVC.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Fax = table.Column<string>(type: "TEXT", nullable: false)
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
                    StockQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    SupplierId = table.Column<long>(type: "INTEGER", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
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
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Total = table.Column<double>(type: "REAL", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    SupplierId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Users_UserId",
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
                name: "InvoicesDetails",
                columns: table => new
                {
                    InvoiceDetailId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImportPrice = table.Column<double>(type: "REAL", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    InvoiceId = table.Column<long>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicesDetails", x => x.InvoiceDetailId);
                    table.ForeignKey(
                        name: "FK_InvoicesDetails_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoicesDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
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
                columns: new[] { "SupplierId", "Address", "Email", "Fax", "Name", "Phone" },
                values: new object[,]
                {
                    { 1L, "Supplier A Address", "supplierA@gmail.com", "123456", "Supplier A", "0987654321" },
                    { 2L, "Supplier B Address", "supplierB@gmail.com", "123457", "Supplier B", "0987654322" },
                    { 3L, "Supplier C Address", "supplierC@gmail.com", "123458", "Supplier C", "0987654323" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Email", "Name", "Password", "Phone", "Role" },
                values: new object[,]
                {
                    { 1L, "Quy Nhon", "huytdqe170235@fpt.edu.vn", "Trần Đức Huy", "123456", "0123456789", "BE" },
                    { 2L, "Quy Nhon", "quynxqe170239@fpt.edu.vn", "Nguyễn Xuân Quý", "123456", "0123456788", "FE" },
                    { 3L, "Quy Nhon", "sangtnqe170193@fpt.edu.vn", "Trần Ngọc Sang", "123456", "0123456787", "FE" },
                    { 4L, "Quy Nhon", "hoangngqe170225@fpt.edu.vn", "Ngô Gia Hoàng", "123456", "0123456786", "BE" },
                    { 5L, "Quy Nhon", "haonnqe170204@fpt.edu.vn", "Nguyễn Nhật Hào", "123456", "0123456785", "FE" },
                    { 6L, "Quy Nhon", "thuanndmqe170240@fpt.edu.vn", "Nguyễn Đào Minh Thuận", "123456", "0123456784", "BE" }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "ExpenseReportId", "Reason", "ReceiverId", "SenderId", "Total" },
                values: new object[,]
                {
                    { 1L, "Enter new Laptops and Phones into warehouse", 2L, 1L, 17599.869999999999 },
                    { 2L, "Enter new Shoes into warehouse", 3L, 1L, 2300.0 },
                    { 3L, "Enter new Shoes into warehouse", 4L, 2L, 1999.8399999999999 }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "SupplierId", "Total", "UserId" },
                values: new object[,]
                {
                    { 1L, 1L, 17599.869999999999, 1L },
                    { 2L, 2L, 2300.0, 1L },
                    { 3L, 3L, 1999.8399999999999, 2L }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "BrandId", "CategoryId", "Description", "Name", "Price", "StockQuantity", "SupplierId", "Unit" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, "18GB Unified Memory, 512GB SSD Storage. Works with iPhone/iPad; Space Black", "Apple 2023 MacBook Pro Laptop M3 Pro", 1399.99, 10, 1L, "Piece" },
                    { 2L, 1L, 2L, "iPhone 15 Pro Max has a strong and light aerospace-grade titanium design with a textured matte-glass back.&nbsp;", "Apple iPhone 15 Pro Max (512 GB)", 1199.99, 3, 1L, "Piece" },
                    { 3L, 3L, 3L, "Designed by Bruce Kilgore and introduced in 1982, the Air Force 1 was the first-ever basketball shoe to feature Nike Air technology", "Air Force 1", 115.0, 20, 2L, "Pair" },
                    { 4L, 4L, 3L, "With these adidas NMD_R1 shoes, all it takes is seconds. Seconds, and you're comfortable, ready to go, out the door.", "NMD_R1 SHOES", 150.0, 0, 3L, "Pair" },
                    { 5L, 4L, 3L, "More than just a shoe, it's a statement. The adidas Forum hit the scene in '84 and gained major love on both the hardwood and in the music biz.", "FORUM LOW SHOES", 99.989999999999995, 16, 3L, "Pair" }
                });

            migrationBuilder.InsertData(
                table: "InvoicesDetails",
                columns: new[] { "InvoiceDetailId", "ImportPrice", "InvoiceId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1L, 1399.99, 1L, 1L, 10 },
                    { 2L, 1199.99, 1L, 2L, 3 },
                    { 3L, 115.0, 2L, 3L, 20 },
                    { 4L, 99.989999999999995, 3L, 5L, 16 }
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

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ReceiverId",
                table: "Expenses",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_SenderId",
                table: "Expenses",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SupplierId",
                table: "Invoices",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicesDetails_InvoiceId",
                table: "InvoicesDetails",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicesDetails_ProductId",
                table: "InvoicesDetails",
                column: "ProductId");

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
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "InvoicesDetails");

            migrationBuilder.DropTable(
                name: "ProductImgs");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
