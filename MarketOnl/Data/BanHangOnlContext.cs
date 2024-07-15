using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MarketOnl.Data;

public partial class BanHangOnlContext : DbContext
{
	public BanHangOnlContext()
	{
	}

	public BanHangOnlContext(DbContextOptions<BanHangOnlContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Account> Accounts { get; set; }

	public virtual DbSet<CatProduct> CatProducts { get; set; }

	public virtual DbSet<News> News { get; set; }

	public virtual DbSet<NewsCat> NewsCats { get; set; }

	public virtual DbSet<Order> Orders { get; set; }

	public virtual DbSet<OrderDetail> OrderDetails { get; set; }

	public virtual DbSet<Product> Products { get; set; }

	public virtual DbSet<Role> Roles { get; set; }

	//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   //  #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
	//=> optionsBuilder.UseSqlServer("Data Source=TranVanLuc;Initial Catalog=BanHangOnl;Integrated Security=True;Trust Server Certificate=True");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Account>(entity =>
		{
			entity.Property(e => e.Email).HasMaxLength(150);
			entity.Property(e => e.Name).HasMaxLength(150);
			entity.Property(e => e.Password).HasMaxLength(150);
			entity.Property(e => e.PhoneNumber).HasMaxLength(150);
			entity.Property(e => e.Picture).HasMaxLength(200);

			entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
				.HasForeignKey(d => d.RoleId)
				.HasConstraintName("FK_Accounts_Roles");
		});

		modelBuilder.Entity<CatProduct>(entity =>
		{
			entity.HasKey(e => e.CatId);

			entity.Property(e => e.Alias).HasMaxLength(250);
			entity.Property(e => e.CatName).HasMaxLength(150);
			entity.Property(e => e.Thumb).HasMaxLength(250);
			entity.Property(e => e.Title).HasMaxLength(250);
		});

		modelBuilder.Entity<News>(entity =>
		{
			entity.Property(e => e.Alias).HasMaxLength(250);
			entity.Property(e => e.Description).HasMaxLength(500);
			entity.Property(e => e.Image).HasMaxLength(250);
			entity.Property(e => e.SeoDescription).HasMaxLength(150);
			entity.Property(e => e.SeoKeywords).HasMaxLength(150);
			entity.Property(e => e.SeoTitle).HasMaxLength(159);
			entity.Property(e => e.Title).HasMaxLength(250);

			entity.HasOne(d => d.NewCat).WithMany(p => p.News)
				.HasForeignKey(d => d.NewCatId)
				.HasConstraintName("FK_News_NewsCat");
		});

		modelBuilder.Entity<NewsCat>(entity =>
		{
			entity.HasKey(e => e.NewCatId);

			entity.ToTable("NewsCat");

			entity.Property(e => e.Alias).HasMaxLength(150);
			entity.Property(e => e.Title).HasMaxLength(150);
		});

		modelBuilder.Entity<Order>(entity =>
		{
			entity.Property(e => e.Address).HasMaxLength(150);
			entity.Property(e => e.CreatedDate).HasColumnType("datetime");
			entity.Property(e => e.CustomerName).HasMaxLength(150);
			entity.Property(e => e.Email).HasMaxLength(150);
			entity.Property(e => e.Phone).HasMaxLength(150);
			entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
		});

		modelBuilder.Entity<OrderDetail>(entity =>
		{
			entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

			entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
				.HasForeignKey(d => d.OrderId)
				.HasConstraintName("FK_OrderDetails_Orders");

			entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
				.HasForeignKey(d => d.ProductId)
				.HasConstraintName("FK_OrderDetails_Products");
		});

		modelBuilder.Entity<Product>(entity =>
		{
			entity.Property(e => e.Alias).HasMaxLength(250);
			entity.Property(e => e.Description).HasMaxLength(500);
			entity.Property(e => e.Picture).HasMaxLength(250);
			entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
			entity.Property(e => e.PriceSale).HasColumnType("decimal(18, 2)");
			entity.Property(e => e.ProductName).HasMaxLength(150);
			entity.Property(e => e.SeoDescription).HasMaxLength(150);
			entity.Property(e => e.SeoKeywords).HasMaxLength(150);
			entity.Property(e => e.SeoTitle).HasMaxLength(150);
			entity.Property(e => e.Title).HasMaxLength(150);

			entity.HasOne(d => d.Cat).WithMany(p => p.Products)
				.HasForeignKey(d => d.CatId)
				.HasConstraintName("FK_Products_CatProducts");
		});

		modelBuilder.Entity<Role>(entity =>
		{
			entity.Property(e => e.Description).HasMaxLength(150);
			entity.Property(e => e.RoleName).HasMaxLength(150);
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
