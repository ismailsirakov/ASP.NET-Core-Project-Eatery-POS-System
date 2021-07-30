namespace EateryPOSSystem.Models.Storekeeper
{
    using EateryPOSSystem.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddMaterialToWarehouseFormModel_1
    {
        public int ProviderId { get; init; }

        public int DocumentNumber { get; init; }

        public int DocumentTypeId { get; init; }

        [DataType(DataType.Date)]
        public DateTime DocumentDate { get; init; } = DateTime.UtcNow.Date;

        public int WarehouseId { get; init; }

        public string ReceiptInfo { get; set; }

        public int UserId { get; init; }

        public IEnumerable<ProviderServiceModel> Providers { get; set; }

        public IEnumerable<WarehouseServiceModel> Warehouses { get; set; }

        public IEnumerable<DocumentTypeServiceModel> DocumentTypes { get; set; }
    }
}
