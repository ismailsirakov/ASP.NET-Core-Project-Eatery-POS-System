namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class WarehouseMaterial
    {
        public WarehouseMaterial()
        {
            WarehouseReceipts = new HashSet<WarehouseReceipt>();
        }
        public int Id { get; set; }

        public int WarehouseId { get; set; }

        public Warehouse Warehouse { get; set; }

        public int MaterialId { get; set; }

        public Material Material { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }

        public ICollection<WarehouseReceipt> WarehouseReceipts { get; set; }

    }
}
