using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TeamLID.TravelExperts.Repository.Domain
{
    public partial class TravelExpertsContext : DbContext
    {
        public TravelExpertsContext()
        {
        }

        public TravelExpertsContext(DbContextOptions<TravelExpertsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Affiliations> Affiliations { get; set; }
        public virtual DbSet<Agencies> Agencies { get; set; }
        public virtual DbSet<Agents> Agents { get; set; }
        public virtual DbSet<BookingDetails> BookingDetails { get; set; }
        public virtual DbSet<Bookings> Bookings { get; set; }
        public virtual DbSet<Classes> Classes { get; set; }
        public virtual DbSet<CreditCards> CreditCards { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<CustomersRewards> CustomersRewards { get; set; }
        public virtual DbSet<Fees> Fees { get; set; }
        public virtual DbSet<Packages> Packages { get; set; }
        public virtual DbSet<PackagesProductsSuppliers> PackagesProductsSuppliers { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductsSuppliers> ProductsSuppliers { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }
        public virtual DbSet<Rewards> Rewards { get; set; }
        public virtual DbSet<SupplierContacts> SupplierContacts { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<TripTypes> TripTypes { get; set; }

        // Unable to generate entity type for table 'dbo.Employees'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.

                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=TravelExperts;Trusted_Connection=True;");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Affiliations>(entity =>
            {
                entity.HasKey(e => e.AffilitationId)
                    .HasName("aaaaaAffiliations_PK")
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.AffilitationId)
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.AffDesc).HasMaxLength(50);

                entity.Property(e => e.AffName).HasMaxLength(50);
            });

            modelBuilder.Entity<Agencies>(entity =>
            {
                entity.HasKey(e => e.AgencyId);

                entity.Property(e => e.AgncyAddress).HasMaxLength(50);

                entity.Property(e => e.AgncyCity).HasMaxLength(50);

                entity.Property(e => e.AgncyCountry).HasMaxLength(50);

                entity.Property(e => e.AgncyFax).HasMaxLength(50);

                entity.Property(e => e.AgncyPhone).HasMaxLength(50);

                entity.Property(e => e.AgncyPostal).HasMaxLength(50);

                entity.Property(e => e.AgncyProv).HasMaxLength(50);
            });

            modelBuilder.Entity<Agents>(entity =>
            {
                entity.HasKey(e => e.AgentId);

                entity.Property(e => e.AgtBusPhone).HasMaxLength(20);

                entity.Property(e => e.AgtEmail).HasMaxLength(50);

                entity.Property(e => e.AgtFirstName).HasMaxLength(20);

                entity.Property(e => e.AgtLastName).HasMaxLength(20);

                entity.Property(e => e.AgtMiddleInitial).HasMaxLength(5);

                entity.Property(e => e.AgtPosition).HasMaxLength(20);

                entity.HasOne(d => d.Agency)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.AgencyId)
                    .HasConstraintName("FK_Agents_Agencies");
            });

            modelBuilder.Entity<BookingDetails>(entity =>
            {
                entity.HasKey(e => e.BookingDetailId)
                    .HasName("aaaaaBookingDetails_PK")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.BookingId)
                    .HasName("BookingsBookingDetails");

                entity.HasIndex(e => e.ClassId)
                    .HasName("ClassesBookingDetails");

                entity.HasIndex(e => e.FeeId)
                    .HasName("FeesBookingDetails");

                entity.HasIndex(e => e.ProductSupplierId)
                    .HasName("ProductSupplierId");

                entity.HasIndex(e => e.RegionId)
                    .HasName("DestinationsBookingDetails");

                entity.Property(e => e.AgencyCommission).HasColumnType("money");

                entity.Property(e => e.BasePrice).HasColumnType("money");

                entity.Property(e => e.BookingId).HasDefaultValueSql("((0))");

                entity.Property(e => e.ClassId).HasMaxLength(5);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Destination).HasMaxLength(255);

                entity.Property(e => e.FeeId).HasMaxLength(10);

                entity.Property(e => e.ProductSupplierId).HasDefaultValueSql("((0))");

                entity.Property(e => e.RegionId).HasMaxLength(5);

                entity.Property(e => e.TripEnd).HasColumnType("datetime");

                entity.Property(e => e.TripStart).HasColumnType("datetime");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK_BookingDetails_Bookings");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_BookingDetails_Classes");

                entity.HasOne(d => d.Fee)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.FeeId)
                    .HasConstraintName("FK_BookingDetails_Fees");

                entity.HasOne(d => d.ProductSupplier)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.ProductSupplierId)
                    .HasConstraintName("FK_BookingDetails_Products_Suppliers");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_BookingDetails_Regions");
            });

            modelBuilder.Entity<Bookings>(entity =>
            {
                entity.HasKey(e => e.BookingId)
                    .HasName("aaaaaBookings_PK")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.CustomerId)
                    .HasName("CustomersBookings");

                entity.HasIndex(e => e.PackageId)
                    .HasName("PackagesBookings");

                entity.HasIndex(e => e.TripTypeId)
                    .HasName("TripTypesBookings");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.BookingNo).HasMaxLength(50);

                entity.Property(e => e.PackageId).HasDefaultValueSql("((0))");

                entity.Property(e => e.TripTypeId).HasMaxLength(1);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("Bookings_FK00");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.PackageId)
                    .HasConstraintName("Bookings_FK01");

                entity.HasOne(d => d.TripType)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.TripTypeId)
                    .HasConstraintName("Bookings_FK02");
            });

            modelBuilder.Entity<Classes>(entity =>
            {
                entity.HasKey(e => e.ClassId)
                    .HasName("aaaaaClasses_PK")
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.ClassId)
                    .HasMaxLength(5)
                    .ValueGeneratedNever();

                entity.Property(e => e.ClassDesc).HasMaxLength(50);

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<CreditCards>(entity =>
            {
                entity.HasKey(e => e.CreditCardId)
                    .HasName("aaaaaCreditCards_PK")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.CustomerId)
                    .HasName("CustomersCreditCards");

                entity.Property(e => e.Ccexpiry)
                    .HasColumnName("CCExpiry")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ccname)
                    .IsRequired()
                    .HasColumnName("CCName")
                    .HasMaxLength(10);

                entity.Property(e => e.Ccnumber)
                    .IsRequired()
                    .HasColumnName("CCNumber")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CreditCards)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CreditCards_FK00");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("aaaaaCustomers_PK")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.AgentId)
                    .HasName("EmployeesCustomers");

                entity.HasIndex(e => e.Username)
                    .HasName("idx_username_notnull")
                    .IsUnique()
                    .HasFilter("([username] IS NOT NULL)");

                entity.Property(e => e.CustAddress)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.CustBusPhone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CustCity)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CustCountry).HasMaxLength(25);

                entity.Property(e => e.CustEmail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CustFirstName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.CustHomePhone).HasMaxLength(20);

                entity.Property(e => e.CustLastName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.CustPostal)
                    .IsRequired()
                    .HasMaxLength(7);

                entity.Property(e => e.CustProv)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_Customers_Agents");
            });

            modelBuilder.Entity<CustomersRewards>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.RewardId })
                    .HasName("aaaaaCustomers_Rewards_PK")
                    .ForSqlServerIsClustered(false);

                entity.ToTable("Customers_Rewards");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("CustomersCustomers_Rewards");

                entity.HasIndex(e => e.RewardId)
                    .HasName("RewardsCustomers_Rewards");

                entity.Property(e => e.RwdNumber)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomersRewards)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Customers_Rewards_FK00");

                entity.HasOne(d => d.Reward)
                    .WithMany(p => p.CustomersRewards)
                    .HasForeignKey(d => d.RewardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Customers_Rewards_FK01");
            });

            modelBuilder.Entity<Fees>(entity =>
            {
                entity.HasKey(e => e.FeeId)
                    .HasName("aaaaaFees_PK")
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.FeeId)
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.FeeAmt).HasColumnType("money");

                entity.Property(e => e.FeeDesc).HasMaxLength(50);

                entity.Property(e => e.FeeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Packages>(entity =>
            {
                entity.HasKey(e => e.PackageId)
                    .HasName("aaaaaPackages_PK")
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.PkgAgencyCommission)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PkgBasePrice).HasColumnType("money");

                entity.Property(e => e.PkgDesc).HasMaxLength(50);

                entity.Property(e => e.PkgEndDate).HasColumnType("datetime");

                entity.Property(e => e.PkgName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PkgStartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PackagesProductsSuppliers>(entity =>
            {
                entity.HasKey(e => new { e.PackageId, e.ProductSupplierId })
                    .HasName("aaaaaPackages_Products_Suppliers_PK")
                    .ForSqlServerIsClustered(false);

                entity.ToTable("Packages_Products_Suppliers");

                entity.HasIndex(e => e.PackageId)
                    .HasName("PackagesPackages_Products_Suppliers");

                entity.HasIndex(e => e.ProductSupplierId)
                    .HasName("ProductSupplierId");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.PackagesProductsSuppliers)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Packages_Products_Supplie_FK00");

                entity.HasOne(d => d.ProductSupplier)
                    .WithMany(p => p.PackagesProductsSuppliers)
                    .HasForeignKey(d => d.ProductSupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Packages_Products_Supplie_FK01");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("aaaaaProducts_PK")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.ProductId)
                    .HasName("ProductId");

                entity.Property(e => e.ProdName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ProductsSuppliers>(entity =>
            {
                entity.HasKey(e => e.ProductSupplierId)
                    .HasName("aaaaaProducts_Suppliers_PK")
                    .ForSqlServerIsClustered(false);

                entity.ToTable("Products_Suppliers");

                entity.HasIndex(e => e.ProductId)
                    .HasName("ProductsProducts_Suppliers1");

                entity.HasIndex(e => e.ProductSupplierId)
                    .HasName("ProductSupplierId");

                entity.HasIndex(e => e.SupplierId)
                    .HasName("SuppliersProducts_Suppliers1");

                entity.Property(e => e.ProductId).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductsSuppliers)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("Products_Suppliers_FK00");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ProductsSuppliers)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("Products_Suppliers_FK01");
            });

            modelBuilder.Entity<Regions>(entity =>
            {
                entity.HasKey(e => e.RegionId)
                    .HasName("aaaaaRegions_PK")
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.RegionId)
                    .HasMaxLength(5)
                    .ValueGeneratedNever();

                entity.Property(e => e.RegionName).HasMaxLength(25);
            });

            modelBuilder.Entity<Rewards>(entity =>
            {
                entity.HasKey(e => e.RewardId)
                    .HasName("aaaaaRewards_PK")
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.RewardId).ValueGeneratedNever();

                entity.Property(e => e.RwdDesc).HasMaxLength(50);

                entity.Property(e => e.RwdName).HasMaxLength(50);
            });

            modelBuilder.Entity<SupplierContacts>(entity =>
            {
                entity.HasKey(e => e.SupplierContactId)
                    .HasName("aaaaaSupplierContacts_PK")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.AffiliationId)
                    .HasName("AffiliationsSupCon");

                entity.HasIndex(e => e.SupplierId)
                    .HasName("SuppliersSupCon");

                entity.Property(e => e.SupplierContactId).ValueGeneratedNever();

                entity.Property(e => e.AffiliationId).HasMaxLength(10);

                entity.Property(e => e.SupConAddress).HasMaxLength(255);

                entity.Property(e => e.SupConBusPhone).HasMaxLength(50);

                entity.Property(e => e.SupConCity).HasMaxLength(255);

                entity.Property(e => e.SupConCompany).HasMaxLength(255);

                entity.Property(e => e.SupConCountry).HasMaxLength(255);

                entity.Property(e => e.SupConEmail).HasMaxLength(255);

                entity.Property(e => e.SupConFax).HasMaxLength(50);

                entity.Property(e => e.SupConFirstName).HasMaxLength(50);

                entity.Property(e => e.SupConLastName).HasMaxLength(50);

                entity.Property(e => e.SupConPostal).HasMaxLength(255);

                entity.Property(e => e.SupConProv).HasMaxLength(255);

                entity.Property(e => e.SupConUrl)
                    .HasColumnName("SupConURL")
                    .HasMaxLength(255);

                entity.Property(e => e.SupplierId).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Affiliation)
                    .WithMany(p => p.SupplierContacts)
                    .HasForeignKey(d => d.AffiliationId)
                    .HasConstraintName("SupplierContacts_FK00");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierContacts)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("SupplierContacts_FK01");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.HasKey(e => e.SupplierId)
                    .HasName("aaaaaSuppliers_PK")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.SupplierId)
                    .HasName("SupplierId");

                entity.Property(e => e.SupplierId).ValueGeneratedNever();

                entity.Property(e => e.SupName).HasMaxLength(255);
            });

            modelBuilder.Entity<TripTypes>(entity =>
            {
                entity.HasKey(e => e.TripTypeId)
                    .HasName("aaaaaTripTypes_PK")
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.TripTypeId)
                    .HasMaxLength(1)
                    .ValueGeneratedNever();

                entity.Property(e => e.Ttname)
                    .HasColumnName("TTName")
                    .HasMaxLength(25);
            });
        }
    }
}
