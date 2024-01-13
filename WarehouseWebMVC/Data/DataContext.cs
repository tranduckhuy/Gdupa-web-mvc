using Microsoft.EntityFrameworkCore;
using WarehouseWebMVC.Models;

namespace WarehouseWebMVC.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoicesDetails { get; set; }
        public DbSet<ExpenseReport> Expenses { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImg> ProductImgs { get; set; }

        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite("A FALLBACK CONNECTION STRING");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Invoices)
                .WithOne(e => e.Supplier)
                .HasForeignKey(e => e.SupplierId)
                .IsRequired();

            {
                // Configure relationships
                modelBuilder.Entity<ExpenseReport>()
                    .HasOne(e => e.Sender)
                    .WithMany(u => u.SentExpenseReports)
                    .HasForeignKey(e => e.SenderId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<ExpenseReport>()
                    .HasOne(e => e.Receiver)
                    .WithMany(u => u.ReceivedExpenseReports)
                    .HasForeignKey(e => e.ReceiverId)
                    .OnDelete(DeleteBehavior.NoAction);
            }

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Warehouses)
                .WithMany(e => e.Products);

            modelBuilder.Entity<InvoiceDetail>()
            .HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
