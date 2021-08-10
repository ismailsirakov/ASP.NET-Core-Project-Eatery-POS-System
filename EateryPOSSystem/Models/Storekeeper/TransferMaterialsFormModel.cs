namespace EateryPOSSystem.Models.Storekeeper
{
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public class TransferMaterialsFormModel
    {
        public int TransferNumber { get; set; }

        public int TransferFromWarehouseId { get; set; }

        public string TransferFromWarehouseName { get; set; }

        public int TransferToWarehouseId { get; set; }

        public string TransferToWarehouseName { get; set; }

        public int TransferedMaterialId { get; set; }

        public string TransferedMaterialName { get; set; }

        public string TransferedMaterialMeasurementName { get; set; }

        public decimal QuantityInWarehouse { get; set; }

        public decimal QuantityToTransfer { get; set; }

        public string UserId { get; set; }

        public IEnumerable<WarehouseServiceModel> Warehouses { get; set; }

        public IEnumerable<WarehouseMaterialServiceModel> WarehouseMaterials { get; set; }

        public IEnumerable<TransferServiceModel> TransferedMaterials { get; set; }
    }
}
