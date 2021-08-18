namespace EateryPOSSystem.Services.Models
{

    public class RecipeServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int StoreProductId { get; set; }

        public string StoreName { get; set; }

        public string StoreProductName { get; set; }

        public int WarehouseId { get; set; }

        public string WarehouseName { get; set; }

        public int WarehouseMaterialId { get; set; }

        public string MaterialName { get; set; }

        public string MeasurementName { get; set; }

        public decimal MaterialQuantity { get; set; }
    }
}
