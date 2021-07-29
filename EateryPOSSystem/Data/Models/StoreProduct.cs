namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class StoreProduct
    {
        public StoreProduct()
        {
            Recipes = new HashSet<Recipe>();
            SoldProducts = new HashSet<SoldProduct>();
        }
        public int Id { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int MeasurementId { get; set; }

        public Measurement Measurement { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }

        public IEnumerable<Recipe> Recipes { get; set; }

        public IEnumerable<SoldProduct> SoldProducts { get; set; }
    }
}
