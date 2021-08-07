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
            CreateMap<ProductDTO, Product>();
            CreateMap<ProviderDTO, Provider>();
            CreateMap<RecipeDTO, Recipe>();
            CreateMap<StoreDTO, Store>();
            CreateMap<StoreProductDTO, StoreProduct>();
            CreateMap<WarehouseDTO, Warehouse>();
            CreateMap<WarehouseReceiptDTO, WarehouseReceipt>();
        }
    }
}
