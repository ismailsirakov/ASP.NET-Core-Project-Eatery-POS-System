namespace EateryPOSSystem.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public interface IReportService
    {
        IEnumerable<BillServiceModel> BillsForDate(DateTime dateTime, bool closed);

        IEnumerable<SoldProductServiceModel> SoldProductsDetailed(int storeId, DateTime fromDate, DateTime toDate);

        IEnumerable<SoldProductServiceModel> SoldProductsCumulative(int storeId, DateTime fromDate, DateTime toDate);

        IEnumerable<WarehouseReceiptServiceModel> WarehouseReceipts(int providerId, DateTime fromDate, DateTime toDate);
    }
}
