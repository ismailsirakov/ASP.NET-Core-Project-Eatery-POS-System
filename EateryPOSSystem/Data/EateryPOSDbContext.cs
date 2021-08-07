namespace EateryPOSSystem.Data
{
    using EateryPOSSystem.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class EateryPOSDbContext : IdentityDbContext
    {

        public EateryPOSDbContext(DbContextOptions<EateryPOSDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; init; }

        public DbSet<Bill> Bills { get; init; }

        public DbSet<City> Cities { get; init; }

        public DbSet<DocumentType> DocumentTypes { get; init; }

        public DbSet<Material> Materials { get; init; }

        public DbSet<Measurement> Measurements { get; init; }

        public DbSet<OrderProduct> OrderProducts { get; init; }

        public DbSet<PaymentType> PaymentTypes { get; init; }

        public DbSet<Position> Positions { get; init; }

        public DbSet<User> POSSystemUsers { get; init; }

        public DbSet<Product> Products { get; init; }

        public DbSet<ProductType> ProductTypes { get; init; }

        public DbSet<Provider> Providers { get; init; }

        public DbSet<Recipe> Recipes { get; init; }

        public DbSet<SoldProduct> SoldProducts { get; init; }

        public DbSet<Store> Stores { get; init; }

        public DbSet<StoreProduct> StoreProducts { get; init; }

        public DbSet<StoreTable> StoreTables { get; init; }

        public DbSet<TempWarehouseReceipt> TempWarehouseReceipts { get; init; }

        public DbSet<Transfer> Transfers { get; init; }

        public DbSet<Warehouse> Warehouses { get; init; }

        public DbSet<WarehouseMaterial> WarehouseMaterials { get; init; }

        public DbSet<WarehouseReceipt> WarehouseReceipts { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasIndex(x => x.AddressDetails)
                .IsUnique();

            modelBuilder.Entity<Bill>(x =>
            {
                x.HasOne(x => x.PaymentType)
                .WithMany(x => x.Bills)
                .HasForeignKey(x => x.PaymentTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Bill>(x =>
            {
                x.HasOne(x => x.User)
                .WithMany(x => x.Bills)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<City>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<DocumentType>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Material>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Material>(x =>
            {
                x.HasOne(x => x.Measurement)
                .WithMany(x => x.Materials)
                .HasForeignKey(x => x.MeasurementId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Measurement>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<PaymentType>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Position>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Product>(x =>
            {
                x.HasOne(x => x.ProductType)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ProductTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductType>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Provider>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Provider>(x =>
            {
                x.HasOne(x => x.Address)
                .WithMany(x => x.Providers)
                .HasForeignKey(x => x.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Provider>(x =>
            {
                x.HasOne(x => x.City)
                .WithMany(x => x.Providers)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Recipe>(x =>
            {
                x.HasOne(x => x.StoreProduct)
                .WithMany(x => x.Recipes)
                .HasForeignKey(x => x.StoreProductId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Recipe>(x =>
            {
                x.HasOne(x => x.WarehouseMaterial)
                .WithMany(x => x.Recipes);
            });

            modelBuilder.Entity<SoldProduct>(x =>
            {
                x.HasOne(x => x.StoreProduct)
                .WithMany(x => x.SoldProducts)
                .HasForeignKey(x => x.StoreProductId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SoldProduct>(x =>
            {
                x.HasOne(x => x.Bill)
                .WithMany(x => x.SoldProducts)
                .HasForeignKey(x => x.BillId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SoldProduct>(x =>
            {
                x.HasOne(x => x.Measurement)
                .WithMany(x => x.SoldProducts)
                .HasForeignKey(x => x.MeasurementId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Store>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<StoreProduct>(x =>
            {
                x.HasOne(x => x.Measurement)
                .WithMany(x => x.StoreProducts)
                .HasForeignKey(x => x.MeasurementId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<StoreProduct>(x =>
            {
                x.HasOne(x => x.Product)
                .WithMany(x => x.StoreProducts)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<StoreProduct>(x =>
            {
                x.HasOne(x => x.Store)
                .WithMany(x => x.StoreProducts)
                .HasForeignKey(x => x.StoreId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Transfer>(x =>
            {
                x.HasOne(x => x.FromWarehouse)
                .WithMany(x => x.TransfersFrom)
                .HasForeignKey(x => x.FromWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
                x.HasOne(x => x.ToWarehouse)
                .WithMany(x => x.TransfersTo)
                .HasForeignKey(x => x.ToWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Transfer>(x =>
            {
                x.HasOne(x => x.Material)
                .WithMany(x => x.Transfers)
                .HasForeignKey(x => x.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);
                x.HasOne(x => x.User)
                .WithMany(x => x.Transfers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(x =>
            {
                x.HasOne(x => x.Position)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.PositionId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>()
                .HasIndex(x => x.UserName)
                .IsUnique();

            modelBuilder.Entity<Warehouse>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<WarehouseReceipt>(x =>
            {
                x.HasOne(x => x.Warehouse)
                .WithMany(x => x.WarehouseReceipts)
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<WarehouseReceipt>(x =>
            {
                x.HasOne(x => x.Material)
                .WithMany(x => x.WarehouseReceipts)
                .HasForeignKey(x => x.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<WarehouseReceipt>(x =>
            {
                x.HasOne(x => x.User)
                .WithMany(x => x.WarehouseReceipts)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<WarehouseReceipt>(x =>
            {
                x.HasOne(x => x.Provider)
                .WithMany(x => x.WarehouseReceipts)
                .HasForeignKey(x => x.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<WarehouseReceipt>(x =>
            {
                x.HasOne(x => x.DocumentType)
                .WithMany(x => x.MaterialReceipts)
                .HasForeignKey(x => x.DocumentTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<WarehouseMaterial>(x =>
            {
                x.HasOne(x => x.Material)
                .WithMany(x => x.WarehouseMaterials)
                .HasForeignKey(x => x.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);
                x.HasOne(x => x.Warehouse)
                .WithMany(x => x.WarehouseMaterials)
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<WarehouseMaterial>().HasKey(x => new { x.WarehouseId, x.MaterialId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
