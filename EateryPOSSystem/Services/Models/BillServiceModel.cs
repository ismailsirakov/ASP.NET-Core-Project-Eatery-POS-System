namespace EateryPOSSystem.Services.Models
{
    using System;
    using System.Collections.Generic;

    public class BillServiceModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string UserBadge { get; set; }

        public DateTime OpenDateTime { get; set; }

        public decimal TotalSum { get; set; }

        public string PaymentTypeName { get; set; }

        public int? PaymentTypeId { get; set; }

        public DateTime? CloseDateTime { get; set; }

        public bool Closed { get; set; }

        public IEnumerable<SoldProductServiceModel> SoldProducts { get; set; }
    }
}
