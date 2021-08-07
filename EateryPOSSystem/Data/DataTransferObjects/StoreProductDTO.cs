namespace EateryPOSSystem.Data.DataTransferObjects
{
    public class StoreProductDTO
    {
        public int StoreId { get; set; }

        public int ProductId { get; set; }

        public int MeasurementId { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }
    }
}
