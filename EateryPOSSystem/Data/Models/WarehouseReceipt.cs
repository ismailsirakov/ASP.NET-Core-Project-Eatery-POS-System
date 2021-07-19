namespace EateryPOSSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class WarehouseReceipt
    {
        public int Id { get; set; }

        public int ReceiptNumber { get; set; }

        public int WarehouseId { get; set; }

        public Warehouse Warehouse { get; set; }

        public int MaterialId { get; set; }

        public Material Material { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }

        public int ProviderId { get; set; }

        public Provider Provider { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int DocumentNumber { get; set; }

        public int DocumentTypeId { get; set; }

        public DocumentType DocumentType { get; set; }

        [DataType(DataType.Date)]
        public DateTime DocumentDate { get; set; }

        public DateTime DateTime { get; set; }

    }
}
