namespace EateryPOSSystem.Services.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class WarehouseReceiptServiceModel
    {
        public int Id { get; set; }

        public int ProviderId { get; set; }

        public string ProviderName { get; set; }

        public int ReceiptNumber { get; set; }

        public int WarehouseId { get; set; }

        public string WarehouseName { get; set; }

        public int MaterialId { get; set; }

        public string MaterialName { get; set; }

        public int MeasurementId { get; set; }

        public string MeasurementName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DocumentDate { get; set; }

        public int DocumentTypeId { get; set; }

        public string DocumentTypeName { get; set; }

        public int DocumentNumber { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
