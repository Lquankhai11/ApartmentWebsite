using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApartmentWebsite.Models
{
    public partial class PRN221_PRJContext : DbContext
    {
        public static PRN221_PRJContext Ins = new PRN221_PRJContext();
        public PRN221_PRJContext()
        {
        }

        public PRN221_PRJContext(DbContextOptions<PRN221_PRJContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Apartment> Apartments { get; set; } = null!;
        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<UserInf> UserInfs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

                if (!optionsBuilder.IsConfigured) { optionsBuilder.UseSqlServer(config.GetConnectionString("value")); }
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //                optionsBuilder.UseSqlServer("Data Source=DESKTOP-P7ULS0M\\SQLEXPRESS;Initial Catalog=PRN221_PRJ; Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminName).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.ToTable("Apartment");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.ApartmentName).HasMaxLength(100);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Video).IsUnicode(false);

                entity.HasMany(d => d.Users)
                    .WithMany(p => p.Apartments)
                    .UsingEntity<Dictionary<string, object>>(
                        "WishList",
                        l => l.HasOne<UserInf>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__WishList__UserId__403A8C7D"),
                        r => r.HasOne<Apartment>().WithMany().HasForeignKey("ApartmentId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__WishList__Apartm__3F466844"),
                        j =>
                        {
                            j.HasKey("ApartmentId", "UserId").HasName("PK__WishList__1AA7DBA0828CE45D");

                            j.ToTable("WishList");
                        });
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("Bill");

                entity.Property(e => e.DatePayment).HasColumnType("date");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 0)");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.ApartmentId)
                    .HasConstraintName("FK__Bill__ApartmentI__4316F928");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Bill__UserId__440B1D61");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Content).HasMaxLength(300);

                entity.Property(e => e.DateComment).HasColumnType("date");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ApartmentId)
                    .HasConstraintName("FK__Comment__Apartme__46E78A0C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Comment__UserId__47DBAE45");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ApartmentId)
                    .HasConstraintName("FK__Feedback__Apartm__4AB81AF0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Feedback__UserId__4BAC3F29");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<UserInf>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__User_Inf__1788CC4C63722F4C");

                entity.ToTable("User_Inf");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserInfs)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__User_Inf__RoleId__3C69FB99");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
