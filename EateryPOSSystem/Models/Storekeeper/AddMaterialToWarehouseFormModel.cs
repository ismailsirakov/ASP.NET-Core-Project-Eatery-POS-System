namespace EateryPOSSystem.Models.Storekeeper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class AddMaterialToWarehouseFormModel
    {
        public int ReceiptNumber { get; init; }

        public int WarehouseId { get; init; }

        public int MaterialId { get; init; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal UnitPrice { get; init; }

        [Column(TypeName = "decimal(18,3)")]

        public decimal Quantity { get; init; }

        public int ProviderId { get; init; }

        public int UserId { get; init; }

        public int DocumentNumber { get; init; }

        public int DocumentTypeId { get; init; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime DocumentDate { get; init; }

        public DateTime DateTime { get; init; }

        public ICollection<ProviderViewModel> Providers { get; set; }

        public ICollection<WarehouseViewModel> Warehouses { get; set; }

        public ICollection<MaterialViewModel> Materials { get; set; }

        public ICollection<DocumentTypeViewModel> DocumentTypes { get; set; }

        public ICollection<MeasurementViewModel> Measurements { get; set; }
    }
}
