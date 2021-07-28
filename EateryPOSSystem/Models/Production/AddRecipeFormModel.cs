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

        public int ProductId { get; set; }

        public int MaterialId { get; set; }

        public string MaterialName { get; set; }

        public decimal MaterialQuantity { get; set; }

        public string RecipeInfo { get; set; }

        public IEnumerable<MaterialServiceModel> Materials { get; set; }

        public IEnumerable<RecipeServiceModel> AddedMaterialsToRecipe { get; set; }

        public IEnumerable<ProductServiceModel> Products { get; set; }
    }
}
