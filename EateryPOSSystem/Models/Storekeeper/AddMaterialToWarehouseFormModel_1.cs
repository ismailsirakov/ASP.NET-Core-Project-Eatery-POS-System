namespace EateryPOSSystem.Models.Storekeeper
{
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
        public ICollection<ProviderViewModel> Providers { get; set; }

        public ICollection<WarehouseViewModel> Warehouses { get; set; }

        public ICollection<DocumentTypeViewModel> DocumentTypes { get; set; }
    }
}
