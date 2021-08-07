namespace EateryPOSSystem.Models.Seller
{
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public class ChooseTableFormModel
    {
        public int Id { get; set; }

        public int ChosenTableNumber { get; set; }

        public string StoreName { get; set; }

        public int TablesInStore { get; set; }

        public IEnumerable<TableServiceModel> TablesWithOpenBills { get; set; }
    }
}
