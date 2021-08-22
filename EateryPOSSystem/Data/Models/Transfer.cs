namespace EateryPOSSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Transfer
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public DateTime DateTime { get; set; }

        public int FromWarehouseId { get; set; }

        public Warehouse FromWarehouse { get; set; }

        public int ToWarehouseId { get; set; }

        public Warehouse ToWarehouse { get; set; }

        public int MaterialId { get; set; }

        public Material Material { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
