using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiRest.Models
{
    public partial class _1718_etu32607_DBContext : DbContext
    {
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Letter> Letter { get; set; }
        public virtual DbSet<Locality> Locality { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Parcel> Parcel { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=vm-sql2.iesn.Be\Stu3ig;Database=1718_etu32607_DB;User Id=1718_etu32607;Password=SeoNY6_00np;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("ADDRESS", "SMART_CITY");

                entity.Property(e => e.BoxNumber).HasMaxLength(5);

                entity.Property(e => e.HouseNumber)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.LocalityIdAddressNavigation)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.LocalityIdAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOCALITY");
            });

            modelBuilder.Entity<Letter>(entity =>
            {
                entity.ToTable("LETTER", "SMART_CITY");

                entity.HasOne(d => d.OrderNumberLetterNavigation)
                    .WithMany(p => p.Letter)
                    .HasForeignKey(d => d.OrderNumberLetter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDER_LETTER");
            });

            modelBuilder.Entity<Locality>(entity =>
            {
                entity.ToTable("LOCALITY", "SMART_CITY");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderNumber);

                entity.ToTable("ORDER", "SMART_CITY");

                entity.Property(e => e.DeliveryType)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.DepositDate).HasColumnType("date");

                entity.Property(e => e.PickUpDate).HasColumnType("date");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.BillingAddressNavigation)
                    .WithMany(p => p.OrderBillingAddressNavigation)
                    .HasForeignKey(d => d.BillingAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BILLING_ADDRESS");

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
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserIdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER");
            });

            modelBuilder.Entity<Parcel>(entity =>
            {
                entity.ToTable("PARCEL", "SMART_CITY");

                entity.Property(e => e.ParcelType)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.HasOne(d => d.OrderNumberParcelNavigation)
                    .WithMany(p => p.Parcel)
                    .HasForeignKey(d => d.OrderNumberParcel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDER_PARCEL");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.CodeRole);

                entity.ToTable("ROLE", "SMART_CITY");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Permission).HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER", "SMART_CITY");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.AddressIdUserNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.AddressIdUser)
                    .HasConstraintName("FK_ADDRESS");

                entity.HasOne(d => d.CodeRoleUserNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.CodeRoleUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLE");
            });
        }
    }
}
