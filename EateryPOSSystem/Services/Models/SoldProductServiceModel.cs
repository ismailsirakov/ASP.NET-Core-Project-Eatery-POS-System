namespace EateryPOSSystem.Services.Models
{
    public class SoldProductServiceModel
    {
        public int Id { get; set; }

        public int BillId { get; set; }

        public int StoreProductId { get; set; }

        public string StoreProductName { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public int MeasurementId { get; set; }

        public string MeasurementName { get; set; }

        
    }
}
