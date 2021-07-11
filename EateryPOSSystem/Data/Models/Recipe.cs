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

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int MaterialId { get; set; }

        public Material Material { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MaterialPrice { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal MaterialQuantity { get; set; }
    }
}
