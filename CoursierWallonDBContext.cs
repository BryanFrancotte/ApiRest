using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiRest
{
    public partial class CoursierWallonDBContext : DbContext
    {
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Letter> Letter { get; set; }
        public virtual DbSet<Locality> Locality { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Parcel> Parcel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=tcp:coursierwallon.database.windows.net,1433;Initial Catalog=CoursierWallonDB;Persist Security Info=False;User ID=BryanAdmin;Password=Yasmine1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("ADDRESS");

                entity.Property(e => e.BoxNumber).HasMaxLength(5);

                entity.Property(e => e.HouseNumber)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.AddressIdUserNavigation)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.AddressIdUser)
                    .HasConstraintName("FK_ADDRESS");
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Letter>(entity =>
            {
                entity.ToTable("LETTER");

                entity.HasOne(d => d.OrderNumberLetterNavigation)
                    .WithMany(p => p.Letter)
                    .HasForeignKey(d => d.OrderNumberLetter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDER_LETTER");
            });

            modelBuilder.Entity<Locality>(entity =>
            {
                entity.ToTable("LOCALITY");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderNumber);

                entity.ToTable("ORDER");

                entity.Property(e => e.CoursierIdOrder)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DeliveryType)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.DepositDate).HasColumnType("date");

                entity.Property(e => e.PickUpDate).HasColumnType("date");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.UserIdOrder)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.BillingAddressNavigation)
                    .WithMany(p => p.OrderBillingAddressNavigation)
                    .HasForeignKey(d => d.BillingAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BILLING_ADDRESS");

                entity.HasOne(d => d.CoursierIdOrderNavigation)
                    .WithMany(p => p.OrderCoursierIdOrderNavigation)
                    .HasForeignKey(d => d.CoursierIdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COURSIER");

                entity.HasOne(d => d.DepositAddressNavigation)
                    .WithMany(p => p.OrderDepositAddressNavigation)
                    .HasForeignKey(d => d.DepositAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEPOSIT_ADDRESS");

                entity.HasOne(d => d.PickUpAddressNavigation)
                    .WithMany(p => p.OrderPickUpAddressNavigation)
                    .HasForeignKey(d => d.PickUpAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PICK_UP_ADDRESS");

                entity.HasOne(d => d.UserIdOrderNavigation)
                    .WithMany(p => p.OrderUserIdOrderNavigation)
                    .HasForeignKey(d => d.UserIdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER");
            });

            modelBuilder.Entity<Parcel>(entity =>
            {
                entity.ToTable("PARCEL");

                entity.Property(e => e.ParcelType)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.HasOne(d => d.OrderNumberParcelNavigation)
                    .WithMany(p => p.Parcel)
                    .HasForeignKey(d => d.OrderNumberParcel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDER_PARCEL");
            });
        }
    }
}
