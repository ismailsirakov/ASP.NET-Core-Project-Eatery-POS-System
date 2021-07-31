namespace EateryPOSSystem.Models.Seller
{
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public class TableFormModel
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public int TableNumber { get; set; }

        public int BillNumber { get; set; }

        public decimal BillTotal { get; set; }

        public string BillUserName { get; set; }

        public IEnumerable<BillServiceModel> OpenBills { get; set; }
    }
}
