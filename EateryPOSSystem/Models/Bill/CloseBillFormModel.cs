namespace EateryPOSSystem.Models.Bill
{
    using System;
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public class CloseBillFormModel
    {
        public int BillId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public DateTime OpenDateTime { get; set; }

        public decimal TotalSum { get; set; }

        public int PaymentTypeId { get; set; }

        public IEnumerable<SoldProductServiceModel> SoldProducts { get; set; }

        public IEnumerable<PaymentTypeServiceModel> PaymentTypes { get; set; }
    }
}
