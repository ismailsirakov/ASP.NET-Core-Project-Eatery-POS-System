namespace EateryPOSSystem.Services.Models
{
    public class StoreProductServiceModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public int MeasurementId { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
