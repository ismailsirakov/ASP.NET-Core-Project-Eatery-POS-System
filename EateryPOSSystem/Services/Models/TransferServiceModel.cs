namespace EateryPOSSystem.Services.Models
{
    public class TransferServiceModel
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public int FromWarehouseId { get; set; }

        public string FromWarehouseName { get; set; }

        public int ToWarehouseId { get; set; }

        public string ToWarehouseName { get; set; }

        public int MaterialId { get; set; }

        public string MaterialName { get; set; }

        public string MeasurementName { get; set; }

        public decimal Quantity { get; set; }

        public int UserId { get; set; }
    }
}
