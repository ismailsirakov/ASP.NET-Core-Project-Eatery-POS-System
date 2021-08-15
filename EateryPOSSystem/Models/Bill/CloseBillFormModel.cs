namespace EateryPOSSystem.Models.Bill
{
    using System;
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public class CloseBillFormModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserBadge { get; set; }

        public DateTime OpenDateTime { get; set; }

        public decimal TotalSum { get; set; }

        public int PaymentTypeId { get; set; }

        public string PaymentTypeName { get; set; }

        public DateTime CloseDateTime { get; set; }

        public IEnumerable<SoldProductServiceModel> SoldProducts { get; set; }

        public IEnumerable<PaymentTypeServiceModel> PaymentTypes { get; set; }
    }
}
