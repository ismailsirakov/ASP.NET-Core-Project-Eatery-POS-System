namespace EateryPOSSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class OrderProduct
    {
        public int Id { get; set; }

        public int BillId { get; set; }

        public int StoreProductId { get; set; }

        public string StoreProductName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }

        public int MeasurementId { get; set; }

        public string MeasurementName { get; set; }
    }
}
