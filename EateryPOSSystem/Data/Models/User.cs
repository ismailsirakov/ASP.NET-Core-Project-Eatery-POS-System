namespace EateryPOSSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using static Data.DataConstants;

    public class User : IdentityUser
    {
        public User()
        {
            Bills = new HashSet<Bill>();
            Transfers = new HashSet<Transfer>();
            WarehouseReceipts = new HashSet<WarehouseReceipt>();
        }

        [MaxLength(UserFirstNameMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(UserLastNameMaxLength)]
        public string LastName { get; set; }

        public int? PositionId { get; set; }

        public Position Position { get; set; }

        public IEnumerable<Bill> Bills { get; set; }

        public IEnumerable<WarehouseReceipt> WarehouseReceipts { get; set; }

        public IEnumerable<Transfer> Transfers { get; set; }
    }
}
