namespace EateryPOSSystem.Models.Storekeeper
{
    using System.Collections.Generic;
    public class AddMaterialToWarehouseViewModel
    {
        public string WarehouseName { get; init; }

        public string ProviderName { get; init; }

        public string DocumnetType { get; init; }

        public string DocumnetDate { get; init; }

        public int DocumentNumber { get; init; }

        public ICollection<MaterialViewModel> Materials { get; set; }

        public ICollection<MeasurementViewModel> Measurements { get; set; }

        public ICollection<WarehouseMaterialViewModel> AddedMaterials { get; set; }
    }
}
