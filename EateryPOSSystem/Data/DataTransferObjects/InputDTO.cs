namespace EateryPOSSystem.Data.DataTransferObjects
{
    using System.Collections.Generic;

    public class InputDTO
    {
        public IEnumerable<AddressDTO> Addresses { get; set; }
        public IEnumerable<CityDTO> Cities { get; set; }
        public IEnumerable<DocumentTypeDTO> DocumentTypes { get; set; }
        public IEnumerable<MaterialDTO> Materials { get; set; }
        public IEnumerable<MeasurementDTO> Measurements { get; set; }
        public IEnumerable<PaymentTypeDTO> PaymentTypes { get; set; }
        public IEnumerable<PositionDTO> Positions { get; set; }
        public IEnumerable<ProductTypeDTO> ProductTypes { get; set; }
        public IEnumerable<ProviderDTO> Providers { get; set; }
        public IEnumerable<StoreDTO> Stores { get; set; }
        public IEnumerable<WarehouseDTO> Warehouses { get; set; }
    }
}
