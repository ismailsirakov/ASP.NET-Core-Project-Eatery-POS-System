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
            SoldProducts = new HashSet<SoldProduct>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public int ProductTypeId { get; set; }

        public ProductType ProductType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int MeasurementId { get; set; }

        public Measurement Measurement { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }

        public ICollection<Recipe> Recipes { get; set; }

        public ICollection<SoldProduct> SoldProducts { get; set; }
    }
}
