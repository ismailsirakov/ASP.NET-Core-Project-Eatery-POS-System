namespace EateryPOSSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SoldProduct
    {
        public int Id { get; set; }

        public int BillId { get; set; }

        public Bill Bill { get; set; }

        public int StoreProductId { get; set; }

        public StoreProduct StoreProduct { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }

        public int MeasurementId { get; set; }

        public Measurement Measurement { get; set; }

        public DateTime DateTime { get; set; }
    }
}
