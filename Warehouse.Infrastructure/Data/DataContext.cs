﻿using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Data;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ImportNote> ImportNotes { get; set; }
    public DbSet<ImportNoteDetail> ImportNoteDetails { get; set; }
    public DbSet<ExpenseReport> Expenses { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Brand> Brand { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImg> ProductImgs { get; set; }
    public DbSet<WarehouseE> Warehouse { get; set; }

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
            .HasMany(e => e.ImportNotes)
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

        modelBuilder.Entity<ImportNote>()
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<ExpenseReport>()
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        modelBuilder.Entity<WarehouseE>()
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

        modelBuilder.Entity<ImportNoteDetail>()
        .HasOne(invoiceDetail => invoiceDetail.Product)
        .WithMany()
        .HasForeignKey(p => p.ProductId);

        modelBuilder.Entity<WarehouseE>()
        .HasOne(warehouse => warehouse.Product)
        .WithMany()
        .HasForeignKey(warehouse => warehouse.ProductId);

        modelBuilder.Seed();
    }

}
