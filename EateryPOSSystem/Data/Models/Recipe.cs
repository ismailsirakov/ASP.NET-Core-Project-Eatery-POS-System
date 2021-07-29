namespace EateryPOSSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Data.DataConstants;

    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(RecipeNameMaxLength)]
        public string Name { get; set; }

        public int StoreProductId { get; set; }

        public StoreProduct StoreProduct { get; set; }

        public int WarehouseId { get; set; }

        public Warehouse Warehouse { get; set; }

        public int WarehouseMaterialId { get; set; }

        public WarehouseMaterial WarehouseMaterial { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal MaterialQuantity { get; set; }
    }
}
