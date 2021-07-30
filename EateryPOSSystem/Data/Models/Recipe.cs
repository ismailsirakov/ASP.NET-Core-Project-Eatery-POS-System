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

        public int WarehouseMaterialWarehouseId { get; set; }

        public int WarehouseMaterialMaterialId { get; set; }

        public WarehouseMaterial WarehouseMaterial { get; set; }

        public StoreProduct StoreProduct { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal MaterialQuantity { get; set; }
    }
}
