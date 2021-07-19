namespace EateryPOSSystem.Models.Storekeeper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class AddMaterialToWarehouseFormModel_2
    {

        public string ReceiptInfo { get; init; }

        public int MaterialId { get; init; }

        public int MeasurementId { get; init; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal UnitPrice { get; init; }

        [Column(TypeName = "decimal(18,3)")]

        public decimal Quantity { get; init; }

        public int UserId { get; init; }

        public ICollection<MaterialViewModel> Materials { get; set; }

        public ICollection<WarehouseMaterialViewModel> AddedMaterials { get; set; }
    }
}
