using System.Collections.Generic;

namespace EateryPOSSystem.Models.Storekeeper
{
    public class WarehouseMaterialViewModel
    {
        public int ReceiptNumber { get; set; }

        public int MaterialId { get; set; }

        public string MaterialName { get; set; }

        public int MeasurementId { get; set; }

        public string MeasurementName { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
