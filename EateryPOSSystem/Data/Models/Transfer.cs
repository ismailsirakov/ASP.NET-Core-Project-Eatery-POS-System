namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Data.DataConstants;

    public class Transfer
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public int FromWarehouseId { get; set; }

        public Warehouse FromWarehouse { get; set; }

        public int ToWarehouseId { get; set; }

        public Warehouse ToWarehouse { get; set; }

        public int MaterialId { get; set; }

        public Material Material { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
