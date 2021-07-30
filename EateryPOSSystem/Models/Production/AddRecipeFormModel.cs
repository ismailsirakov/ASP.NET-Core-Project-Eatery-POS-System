namespace EateryPOSSystem.Models.Production
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using EateryPOSSystem.Services.Models;
    using static Data.DataConstants;

    public class AddRecipeFormModel
    {
        [Required]
        [MaxLength(RecipeNameMaxLength)]
        public string Name { get; set; }

        public int StoreProductId { get; set; }

        public int WarehouseId { get; set; }

        public int WarehouseMaterialMaterialId { get; set; }

        public int WarehouseMaterialWarehouseId { get; set; }

        public string MaterialName { get; set; }

        public decimal MaterialQuantity { get; set; }

        public string RecipeInfo { get; set; }

        public IEnumerable<WarehouseMaterialServiceModel> WarehouseMaterials { get; set; }

        public IEnumerable<RecipeServiceModel> AddedMaterialsToRecipe { get; set; }

        public IEnumerable<StoreProductServiceModel> StoreProducts { get; set; }

        public IEnumerable<WarehouseServiceModel> Warehouses { get; set; }
    }
}
