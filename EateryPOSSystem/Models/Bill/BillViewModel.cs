namespace EateryPOSSystem.Models.Bill
{
    using System;
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public class BillViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public DateTime OpenDateTime { get; set; }

        public decimal TotalSum { get; set; }

        public int PaymentTypeId { get; set; }

        public string PaymentTypeName { get; set; }

        public DateTime CloseDateTime { get; set; }

        public bool Closed { get; set; }

        public IEnumerable<SoldProductServiceModel> SoldProducts { get; set; }
    }
}
