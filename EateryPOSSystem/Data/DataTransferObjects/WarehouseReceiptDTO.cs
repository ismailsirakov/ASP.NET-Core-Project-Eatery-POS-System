using System;

namespace EateryPOSSystem.Data.DataTransferObjects
{
    public class WarehouseReceiptDTO
    {
        public int ReceiptNumber { get; set; }

        public int WarehouseId { get; set; }

        public int MaterialId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }

        public int ProviderId { get; set; }

        public string UserId { get; set; }

        public int DocumentNumber { get; set; }

        public int DocumentTypeId { get; set; }

        public DateTime DocumentDate { get; set; }

        public DateTime DateTime { get; set; }
    }
}
