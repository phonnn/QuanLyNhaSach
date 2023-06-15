using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.DataAccess
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Bill> Bills { get; set; }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<BookType> BookTypes { get; set; }

        public virtual DbSet<BuyReceipt> BuyReceipts { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Parameter> Parameters { get; set; }

        public virtual DbSet<Receipt> Receipts { get; set; }

        public virtual DbSet<ReceiptBook> ReceiptBooks { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<SellReceipt> SellReceipts { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Author__3213E83F1AFCECA6");

                entity.ToTable("Author");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("(newsequentialid())")
                    .HasColumnName("id");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("createdAt");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("updatedAt");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Bill__3213E83F114393A0");

                entity.ToTable("Bill");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("(newsequentialid())")
                    .HasColumnName("id");
                entity.Property(e => e.Amount).HasColumnName("amount");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("createdAt");
                entity.Property(e => e.Customer).HasColumnName("customer");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.CustomerNavigation).WithMany(p => p.Bills)
                    .HasForeignKey(d => d.Customer)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Bill__customer__5165187F");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Book__3213E83F07DE7726");

                entity.ToTable("Book");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("(newsequentialid())")
                    .HasColumnName("id");
                entity.Property(e => e.Author).HasColumnName("author");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("createdAt");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.Type).HasColumnName("type");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Books)
                    .HasForeignKey(d => d.Author)
                    .HasConstraintName("FK__Book__author__45F365D3");

                entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Books)
                    .HasForeignKey(d => d.Type)
                    .HasConstraintName("FK__Book__type__44FF419A");
            });

            modelBuilder.Entity<BookType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__BookType__3213E83F45F5BBDE");

                entity.ToTable("BookType");

                entity.HasIndex(e => e.Name, "UQ__BookType__72E12F1BE09CF7FE").IsUnique();

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("(newsequentialid())")
                    .HasColumnName("id");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("createdAt");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("updatedAt");
            });

            modelBuilder.Entity<BuyReceipt>(entity =>
            {
                entity.ToTable("BuyReceipt");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
                entity.Property(e => e.User).HasColumnName("user");

                entity.HasOne(d => d.IdNavigation).WithOne(p => p.BuyReceipt)
                    .HasForeignKey<BuyReceipt>(d => d.Id)
                    .HasConstraintName("FK__BuyReceipt__id__5DCAEF64");

                entity.HasOne(d => d.UserNavigation).WithMany(p => p.BuyReceipts)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("FK__BuyReceipt__user__5EBF139D");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Customer__3213E83FCBA952FB");

                entity.ToTable("Customer");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("(newsequentialid())")
                    .HasColumnName("id");
                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("address");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("createdAt");
                entity.Property(e => e.Debt)
                    .HasDefaultValueSql("((0))")
                    .HasColumnName("debt");
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
                entity.Property(e => e.Number)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("number");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("updatedAt");
            });

            modelBuilder.Entity<Parameter>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Paramete__3213E83F3D94B694");

                entity.ToTable("Parameter");

                entity.HasIndex(e => e.Name, "UQ__Paramete__72E12F1BE988F86D").IsUnique();

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("(newsequentialid())")
                    .HasColumnName("id");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("createdAt");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("updatedAt");
                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Receipt__3213E83F86179120");

                entity.ToTable("Receipt");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("(newsequentialid())")
                    .HasColumnName("id");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("createdAt");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("updatedAt");
            });

            modelBuilder.Entity<ReceiptBook>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ReceiptB__3213E83F4436C8F3");

                entity.ToTable("ReceiptBook");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("(newsequentialid())")
                    .HasColumnName("id");
                entity.Property(e => e.Amount).HasColumnName("amount");
                entity.Property(e => e.Book).HasColumnName("book");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("createdAt");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.Receipt).HasColumnName("receipt");
                entity.Property(e => e.Total).HasColumnName("total");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.BookNavigation).WithMany(p => p.ReceiptBooks)
                    .HasForeignKey(d => d.Book)
                    .HasConstraintName("FK__ReceiptBoo__book__71D1E811");

                entity.HasOne(d => d.ReceiptNavigation).WithMany(p => p.ReceiptBooks)
                    .HasForeignKey(d => d.Receipt)
                    .HasConstraintName("FK__ReceiptBo__recei__70DDC3D8");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Role__3213E83FB57DEB75");

                entity.ToTable("Role");

                entity.HasIndex(e => e.Name, "UQ__Role__72E12F1BC4CC8984").IsUnique();

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("(newsequentialid())")
                    .HasColumnName("id");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("createdAt");
                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("updatedAt");
            });

            modelBuilder.Entity<SellReceipt>(entity =>
            {
                entity.ToTable("SellReceipt");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
                entity.Property(e => e.Customer).HasColumnName("customer");

                entity.HasOne(d => d.CustomerNavigation).WithMany(p => p.SellReceipts)
                    .HasForeignKey(d => d.Customer)
                    .HasConstraintName("FK__SellRecei__custo__628FA481");

                entity.HasOne(d => d.IdNavigation).WithOne(p => p.SellReceipt)
                    .HasForeignKey<SellReceipt>(d => d.Id)
                    .HasConstraintName("FK__SellReceipt__id__619B8048");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__User__3213E83F62AF6DD0");

                entity.ToTable("User");

                entity.HasIndex(e => e.Username, "UQ__User__F3DBC572D5755CEE").IsUnique();

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("(newsequentialid())")
                    .HasColumnName("id");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("createdAt");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("password");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("updatedAt");
                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__UserRole__3213E83F3683FCE3");

                entity.ToTable("UserRole");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("(newsequentialid())")
                    .HasColumnName("id");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("createdAt");
                entity.Property(e => e.Role).HasColumnName("role");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("updatedAt");
                entity.Property(e => e.User).HasColumnName("user");

                entity.HasOne(d => d.RoleNavigation).WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.Role)
                    .HasConstraintName("FK__UserRole__role__34C8D9D1");

                entity.HasOne(d => d.UserNavigation).WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("FK__UserRole__user__33D4B598");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}


