using System.ComponentModel.DataAnnotations.Schema;

namespace EateryPOSSystem.Data.Models
{
    public class WarehouseMaterial
    {
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }

    }
}
