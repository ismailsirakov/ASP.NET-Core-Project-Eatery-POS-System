namespace EateryPOSSystem.Models.Storekeeper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using EateryPOSSystem.Services.Models;

    public class AddMaterialToWarehouseFormModel_1
    {
        public int ProviderId { get; init; }

        public int DocumentNumber { get; init; }

        public int DocumentTypeId { get; init; }

        [DataType(DataType.Date)]
        public DateTime DocumentDate { get; init; } = DateTime.UtcNow.Date;

        public int WarehouseId { get; init; }

        public string ReceiptInfo { get; set; }

        public string UserId { get; init; }

        public IEnumerable<ProviderServiceModel> Providers { get; set; }

        public IEnumerable<WarehouseServiceModel> Warehouses { get; set; }

        public IEnumerable<DocumentTypeServiceModel> DocumentTypes { get; set; }
    }
}
