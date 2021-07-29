namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Warehouse
    {
        public Warehouse()
        {
            Recipes = new HashSet<Recipe>();
            WarehouseMaterials = new HashSet<WarehouseMaterial>();
            WarehouseReceipts = new HashSet<WarehouseReceipt>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(WarehouseNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Recipe> Recipes { get; set; }

        public IEnumerable<WarehouseMaterial> WarehouseMaterials { get; set; }

        public IEnumerable<WarehouseReceipt> WarehouseReceipts { get; set; }
    }
}
