namespace EateryPOSSystem.Data.DataTransferObjects
{
    public class RecipeDTO
    {
        public string Name { get; set; }

        public int StoreProductId { get; set; }

        public int WarehouseMaterialWarehouseId { get; set; }

        public int WarehouseMaterialMaterialId { get; set; }

        public decimal MaterialQuantity { get; set; }
    }
}
