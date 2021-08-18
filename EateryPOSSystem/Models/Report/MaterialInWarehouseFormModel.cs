namespace EateryPOSSystem.Models.Report
{
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public class MaterialInWarehouseFormModel
    {

        public int WarehouseId { get; set; }

        public string WarehouseName { get; set; }
        
        public IEnumerable<WarehouseServiceModel> Warehouses { get; set; }

        public IEnumerable<WarehouseMaterialServiceModel> MaterialsInWarehouse { get; set; }
    }
}
