namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class WarehouseMaterial
    {
        public WarehouseMaterial()
        {
            Recipes = new HashSet<Recipe>();
        }

        public int Id { get; set; }

       public int WarehouseId { get; set; }

        public Warehouse Warehouse { get; set; }

        public int MaterialId { get; set; }

        public Material Material { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }

        public IEnumerable<Recipe> Recipes { get; set; }

    }
}
