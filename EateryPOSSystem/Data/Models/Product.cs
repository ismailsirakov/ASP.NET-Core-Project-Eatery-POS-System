namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Data.DataConstants;

    public class Product
    {
        public Product()
        {
            Recipes = new HashSet<Recipe>();
            StoreProducts = new HashSet<StoreProduct>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; }

        public int ProductTypeId { get; set; }

        public ProductType ProductType { get; set; }

        public ICollection<Recipe> Recipes { get; set; }

        public ICollection<StoreProduct> StoreProducts { get; set; }
    }
}
