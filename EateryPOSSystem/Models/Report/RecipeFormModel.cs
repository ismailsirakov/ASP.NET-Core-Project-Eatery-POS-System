namespace EateryPOSSystem.Models.Report
{
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public class RecipeFormModel
    {
        public int StoreProductId { get; set; }

        public string ProductName { get; set; }

        public string StoreName { get; set; }

        public string RecipeName { get; set; }

        public IEnumerable<RecipeServiceModel> Recipes { get; set; }

    }
}
