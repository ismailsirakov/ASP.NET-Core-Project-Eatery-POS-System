namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [NotMapped]
    public class Import
    {
        public IEnumerable<Address> Addresses { get; set; }

        public IEnumerable<City> Cities { get; set; }

        public IEnumerable<DocumentType> DocumentTypes { get; set; }

        public IEnumerable<Material> Materials { get; set; }

        public IEnumerable<Measurement> Measurements { get; set; }

        public IEnumerable<PaymentType> PaymentTypes { get; set; }

        public IEnumerable<Position> Positions { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<ProductType> ProductTypes { get; set; }

        public IEnumerable<Provider> Providers { get; set; }

        public IEnumerable<Recipe> Recipes { get; set; }

        public IEnumerable<Store> Stores { get; set; }

        public IEnumerable<StoreProduct> StoreProducts { get; set; }

        public IEnumerable<Warehouse> Warehouses { get; set; }

        public IEnumerable<WarehouseReceipt> warehouseReceipts { get; set; }
    }
}
