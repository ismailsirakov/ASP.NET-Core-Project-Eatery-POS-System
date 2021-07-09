namespace EateryPOSSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Bill
    {
        public Bill()
        {
            SoldProducts = new HashSet<SoldProduct>();
        }
        public int Id { get; set; }

        public int BillNumber { get; set; }

        public DateTime BillOpenDateTime { get; set; }

        public DateTime BillCloseDateTime { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int PaymentTypeId { get; set; }

        public PaymentType PaymentType { get; set; }

        public bool IsClosed { get; set; }

        public ICollection<SoldProduct> SoldProducts { get; set; }
    }
}

