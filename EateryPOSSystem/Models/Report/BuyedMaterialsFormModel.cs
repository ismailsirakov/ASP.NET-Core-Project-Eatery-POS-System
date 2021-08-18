namespace EateryPOSSystem.Models.Report
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using EateryPOSSystem.Services.Models;

    public class BuyedMaterialsFormModel
    {
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; } = DateTime.UtcNow.Date;

        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; } = DateTime.UtcNow.Date;

        public int ProviderId { get; set; }

        public string ProviderName { get; set; }

        public IEnumerable<WarehouseReceiptServiceModel> Receipts { get; set; }

        public IEnumerable<ProviderServiceModel> Providers { get; set; }
    }
}
