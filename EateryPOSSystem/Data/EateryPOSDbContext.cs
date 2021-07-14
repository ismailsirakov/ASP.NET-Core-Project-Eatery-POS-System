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

        public DbSet<PaymentType> PaymentTypes { get; init; }

        public DbSet<Position> Positions { get; init; }

        public DbSet<User> POSSystemUsers { get; init; }

        public DbSet<Product> Products { get; init; }

        public DbSet<ProductType> ProductTypes { get; init; }

        public DbSet<Provider> Providers { get; init; }

        public DbSet<Recipe> Recipes { get; init; }

        public DbSet<SoldProduct> SoldProducts { get; init; }

        public DbSet<Store> Stores { get; init; }

        public DbSet<Warehouse> Warehouses { get; init; }

        public DbSet<WarehouseMaterial> WarehouseMaterials { get; init; }

        public DbSet<WarehouseReceipt> WarehouseReceipts { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(x =>
            {
                x.HasOne(x => x.City)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);
            });

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

            modelBuilder.Entity<Material>(x =>
            {
                x.HasOne(x => x.Measurement)
                .WithMany(x => x.Materials)
                .HasForeignKey(x => x.MeasurementId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            
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


            modelBuilder.Entity<Product>(x =>
            {
                x.HasOne(x => x.Measurement)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.MeasurementId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Product>(x =>
            {
                x.HasOne(x => x.ProductType)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ProductTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Product>(x =>
            {
                x.HasOne(x => x.Store)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.StoreId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Provider>(x =>
            {
                x.HasOne(x => x.Address)
                .WithMany(x => x.Providers)
                .HasForeignKey(x => x.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Recipe>(x =>
            {
                x.HasOne(x => x.Product)
                .WithMany(x => x.Recipes)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
                x.HasOne(x => x.Material)
                .WithMany(x => x.Recipes)
                .HasForeignKey(x => x.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SoldProduct>(x =>
            {
                x.HasOne(x => x.Product)
                .WithMany(x => x.SoldProducts)
                .HasForeignKey(x => x.ProductId)
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

            modelBuilder.Entity<User>(x =>
            {
                x.HasOne(x => x.Position)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.PositionId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<WarehouseMaterial>().HasKey(x => new { x.WarehouseId, x.MaterialId });

            modelBuilder.Entity<WarehouseMaterial>(x =>
            {
                x.HasOne(x => x.Material)
                .WithMany(x => x.WarehouseMaterials)
                .HasForeignKey(x => x.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<WarehouseMaterial>(x =>
            {
                x.HasOne(x => x.Warehouse)
                .WithMany(x => x.WarehouseMaterials)
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
