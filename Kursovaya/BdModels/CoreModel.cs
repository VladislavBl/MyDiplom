using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Diplom.BdModels;

public partial class CoreModel : DbContext
{

    private static CoreModel instanse;

    public static CoreModel init()
    {
        if (instanse == null)
        {
            instanse = new CoreModel();
        }
        return instanse; 
    }

    public CoreModel()
    {
    }

    public CoreModel(DbContextOptions<CoreModel> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderGood> OrderGoods { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=cfif31.ru;port=3306;user id=ISPr23-36_BlinovVA;password=ISPr23-36_BlinovVA;database=ISPr23-36_BlinovVA_Kursovaya3kurs;character set=utf8", ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PRIMARY");

            entity.ToTable("Order");

            entity.HasIndex(e => e.Buyer, "FK_Buyer_idx");

            entity.Property(e => e.IdOrder).HasColumnName("idOrder");
            entity.Property(e => e.OrderSum).HasPrecision(10, 2);

            entity.HasOne(d => d.BuyerNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Buyer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Buyer");
        });

        modelBuilder.Entity<OrderGood>(entity =>
        {
            entity.HasKey(e => e.Idorders).HasName("PRIMARY");

            entity.ToTable("Order goods");

            entity.HasIndex(e => e.Idproduct, "FK_Product_idx");

            entity.Property(e => e.Idorders)
                .ValueGeneratedNever()
                .HasColumnName("idorders");
            entity.Property(e => e.Idproduct).HasColumnName("idproduct");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Sum)
                .HasPrecision(10, 2)
                .HasColumnName("sum");

            entity.HasOne(d => d.IdordersNavigation)
                .WithMany(p => p.OrderGoods)
                .HasForeignKey(d => d.Idorders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order");

            entity.HasOne(d => d.IdproductNavigation)
                .WithMany(p => p.OrderGoods)
                .HasForeignKey(d => d.Idproduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PRIMARY");

            entity.ToTable("Product");

            entity.Property(e => e.IdProduct).HasColumnName("idProduct");
            entity.Property(e => e.ProductName).HasMaxLength(45);
            entity.Property(e => e.ProductPrice).HasPrecision(10, 2);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUsers).HasName("PRIMARY");

            entity.Property(e => e.IdUsers).HasColumnName("idUsers");
            entity.Property(e => e.Lastname).HasMaxLength(45);
            entity.Property(e => e.Login).HasMaxLength(45);
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.Password).HasMaxLength(45);
            entity.Property(e => e.Surname).HasMaxLength(45);
            entity.Property(e => e.UsersStatus).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
