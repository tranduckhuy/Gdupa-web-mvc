using Microsoft.EntityFrameworkCore;
using WarehouseWebMVC.Models.Domain;

namespace WarehouseWebMVC.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoicesDetails { get; set; }
        public DbSet<ExpenseReport> Expenses { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Brand> Brand { get; set; }
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

            // Default DateTime
            modelBuilder.Entity<User>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Product>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP"); 

            modelBuilder.Entity<Product>()
                .Property(e => e.ModifiedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Invoice>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<ExpenseReport>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Warehouse>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            // ---

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

            modelBuilder.Entity<InvoiceDetail>()
            .HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Seed();
        }

    }
}
