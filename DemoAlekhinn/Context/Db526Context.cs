using System;
using System.Collections.Generic;
using DemoAlekhinn.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAlekhinn.Context;

public partial class Db526Context : DbContext
{
    public Db526Context()
    {
    }

    public Db526Context(DbContextOptions<Db526Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Costcalculation> Costcalculations { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Customerorder> Customerorders { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Orderitem> Orderitems { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<Production> Productions { get; set; }

    public virtual DbSet<Productspecification> Productspecifications { get; set; }

    public virtual DbSet<Specification> Specifications { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=lorksipt.ru;Database=db526;Username=user526;password=73518");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Costcalculation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("costcalculation_pkey");

            entity.ToTable("costcalculation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Calculationdate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("calculationdate");
            entity.Property(e => e.Cost)
                .HasPrecision(15, 2)
                .HasColumnName("cost");
            entity.Property(e => e.Materialid).HasColumnName("materialid");
            entity.Property(e => e.Price)
                .HasPrecision(15, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity)
                .HasPrecision(15, 3)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Material).WithMany(p => p.Costcalculations)
                .HasForeignKey(d => d.Materialid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("costcalculation_materialid_fkey");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.Property(e => e.Id)
                .HasMaxLength(9)
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .HasColumnName("address");
            entity.Property(e => e.Inn)
                .HasMaxLength(12)
                .HasColumnName("inn");
            entity.Property(e => e.Isbuyer).HasColumnName("isbuyer");
            entity.Property(e => e.Issalesman).HasColumnName("issalesman");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Customerorder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customerorders_pkey");

            entity.ToTable("customerorders");

            entity.HasIndex(e => e.Customerid, "idx_customer_orders_customer_id");

            entity.HasIndex(e => e.Orderdate, "idx_customer_orders_order_date");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Customerid)
                .HasMaxLength(9)
                .HasColumnName("customerid");
            entity.Property(e => e.Orderdate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("orderdate");

            entity.HasOne(d => d.Customer).WithMany(p => p.Customerorders)
                .HasForeignKey(d => d.Customerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customerorders_customerid_fkey");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("materials_pkey");

            entity.ToTable("materials");

            entity.HasIndex(e => e.Name, "materials_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Unit)
                .HasMaxLength(10)
                .HasColumnName("unit");
        });

        modelBuilder.Entity<Orderitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orderitems_pkey");

            entity.ToTable("orderitems");

            entity.HasIndex(e => e.Orderid, "idx_order_items_order_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(15, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Materialid).HasColumnName("materialid");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Price)
                .HasPrecision(15, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity)
                .HasPrecision(15, 3)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Material).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.Materialid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitems_materialid_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitems_orderid_fkey");
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.Materialid).HasName("prices_pkey");

            entity.ToTable("prices");

            entity.HasIndex(e => e.Materialid, "idx_prices_material_id");

            entity.Property(e => e.Materialid)
                .ValueGeneratedNever()
                .HasColumnName("materialid");
            entity.Property(e => e.Price1)
                .HasPrecision(15, 2)
                .HasColumnName("price");

            entity.HasOne(d => d.Material).WithOne(p => p.Price)
                .HasForeignKey<Price>(d => d.Materialid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prices_materialid_fkey");
        });

        modelBuilder.Entity<Production>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("production_pkey");

            entity.ToTable("production");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Materialid).HasColumnName("materialid");
            entity.Property(e => e.Quantity)
                .HasPrecision(15, 3)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Material).WithMany(p => p.Productions)
                .HasForeignKey(d => d.Materialid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("production_materialid_fkey");
        });

        modelBuilder.Entity<Productspecification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("productspecification_pkey");

            entity.ToTable("productspecification");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Materialid).HasColumnName("materialid");
            entity.Property(e => e.Productcode)
                .HasMaxLength(50)
                .HasColumnName("productcode");
            entity.Property(e => e.Productname)
                .HasMaxLength(100)
                .HasColumnName("productname");
            entity.Property(e => e.Quantity)
                .HasPrecision(15, 3)
                .HasColumnName("quantity");
            entity.Property(e => e.Unit)
                .HasMaxLength(10)
                .HasColumnName("unit");

            entity.HasOne(d => d.Material).WithMany(p => p.Productspecifications)
                .HasForeignKey(d => d.Materialid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("productspecification_materialid_fkey");
        });

        modelBuilder.Entity<Specification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("specification_pkey");

            entity.ToTable("specification");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Materialid).HasColumnName("materialid");
            entity.Property(e => e.Quantity)
                .HasPrecision(15, 3)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Material).WithMany(p => p.Specifications)
                .HasForeignKey(d => d.Materialid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("specification_materialid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Login, "users_login_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
