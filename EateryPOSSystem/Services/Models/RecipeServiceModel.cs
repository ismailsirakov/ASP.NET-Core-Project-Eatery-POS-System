namespace EateryPOSSystem.Services.Models
{
    using System.Collections.Generic;

    public class RecipeServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProductId { get; set; }

        public int MaterialId { get; set; }

        public string MaterialName { get; set; }

        public string MeasurementName { get; set; }

        public decimal MaterialQuantity { get; set; }
    }
}
