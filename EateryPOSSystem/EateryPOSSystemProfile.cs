namespace EateryPOSSystem
{
    using AutoMapper;
    using EateryPOSSystem.Data.DataTransferObjects;
    using EateryPOSSystem.Data.Models;

    public class EateryPOSSystemProfile : Profile
    {
        public EateryPOSSystemProfile()
        {
            CreateMap<InputDTO, Input>();
            CreateMap<AddressDTO, Address>();
            CreateMap<CityDTO, City>();
            CreateMap<DocumentTypeDTO, DocumentType>();
            CreateMap<MaterialDTO, Material>();
            CreateMap<MeasurementDTO, Measurement>();
            CreateMap<PaymentTypeDTO, PaymentType>();
            CreateMap<PositionDTO, Position>();
            CreateMap<ProductTypeDTO, ProductType>();
            CreateMap<ProviderDTO, Provider>();
            CreateMap<StoreDTO, Store>();
            CreateMap<WarehouseDTO, Warehouse>();
        }
    }
}
