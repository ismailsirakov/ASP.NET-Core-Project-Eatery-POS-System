namespace EateryPOSSystem.Models.Storekeeper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EateryPOSSystem.Services.Models;

    public class AddMaterialToWarehouseFormModel_2
    {
        public string ReceiptInfo { get; init; }

        public int MaterialId { get; init; }

        public int ProviderId { get; init; }

        public int DocumentTypeId { get; init; }

        public int DocumentNumber { get; init; }

        public int WarehouseId { get; init; }

        [DataType(DataType.Date)]
        public DateTime DocumentDate { get; init; } = DateTime.UtcNow.Date;

        [Column(TypeName = "decimal(18,2)")]

        public decimal UnitPrice { get; init; }

        [Column(TypeName = "decimal(18,3)")]

        public decimal Quantity { get; init; }

        public int UserId { get; init; }

        public IEnumerable<MaterialServiceModel> Materials { get; set; }

        public IEnumerable<WarehouseMaterialServiceModel> AddedMaterials { get; set; }
    }
}
